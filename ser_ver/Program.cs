using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System;
using ser_ver;
using System.Collections.Generic;
using курсач;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Runtime.CompilerServices;

//private volatile CancellationTokenSource _cts;
int q = 1;
Console.WriteLine("Сервер запущен. Ожидание подключений...");
IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
//IPEndPoint ipPointAdmin = new IPEndPoint(IPAddress.Any, 1111);
using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//using Socket socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
socket.Bind(ipPoint);
socket.Listen();
bool active = true;
bool activeClient = false;
bool activeAdmin = false;
Socket adminMess = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
Socket clientMess = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
Dictionary<string, List<string>> newMessages = new Dictionary<string, List<string>>()
{
    {"client", null },
    {"admin", null}
};

MainMethod();

//using Socket client = await socket1.AcceptAsync();
//
//Thread th1 = new Thread(() =>Thread1());
//th1.Start();
//socket1.Bind(ipPointAdmin);
/*socket.Listen(1000);
using Socket admin = await socket.AcceptAsync();
Thread th2 = new Thread(() =>Thread2());
th2.Start();*/

void MainMethod()
{
    while (active)
    {
        try
        {
            Socket listenerAccept = socket.Accept();
            if (listenerAccept != null)
            {
                var buffer = new byte[1_024];
                var received = listenerAccept.Receive(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);
                switch (response)
                {
                    case "admin":
                        Task.Run(() => Thread2(listenerAccept));
                        break;
                    case "client":
                        Task.Run(() => Thread1(listenerAccept));
                        break;
                    case "messageAdmin":
                        Task.Run(() => MessageAdmin(listenerAccept));
                        break;
                    case "messageClient":
                        Task.Run(() => MessageClient(listenerAccept));
                        break;
                }
            }
        }
        catch { }
    }
}

void MessageAdmin(Socket admin)
{
    adminMess = admin;
    activeAdmin = true;
    while(admin.Connected) 
    {
        try
        {
            Console.WriteLine("Admin message connect");
            var buffer = new byte[1_1000];
            var received = admin.Receive(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);
            Request request = JsonSerializer.Deserialize<Request>(response);
            if (request.command == "save")
            {
                Dictionary<string, List<string>> allMessages = Serialization.AllMassegesOut();
                allMessages[JsonSerializer.Deserialize<List<string>>(request.data)[0]] = JsonSerializer.Deserialize<List<string>>
                    (JsonSerializer.Deserialize<List<string>>(request.data)[1]);
                Serialization.AllMassegesIn(allMessages);
            }
            else if (request.command == "newMessage")
            {
                if (activeClient)
                {
                    var messageBytes = Encoding.UTF8.GetBytes(request.data);
                    clientMess.Send(messageBytes, SocketFlags.None);
                }
            }
        }
        catch (System.Net.Sockets.SocketException)
        {

        }
        catch (System.Text.Json.JsonException)
        {

        }
    }
    if (!admin.Connected)
    {
        activeAdmin = false;
        MainMethod();
    }
}

void MessageClient(Socket client)
{
    clientMess = client;
    activeClient = true;
    while (client.Connected)
    {
        try
        {
            Console.WriteLine("Client message connect");
            var buffer = new byte[1_1000];
            var received = client.Receive(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);
            Request request = JsonSerializer.Deserialize<Request>(response);
            if (request.command == "save")
            {
                Dictionary<string, List< string>> allMessages = Serialization.AllMassegesOut();
                allMessages[JsonSerializer.Deserialize<List<string>>(request.data)[0]] = JsonSerializer.Deserialize<List<string>>
                    (JsonSerializer.Deserialize<List<string>>(request.data)[1]);
                Serialization.AllMassegesIn(allMessages);
            }
            else if (request.command == "newMessage")
            {
                if (activeAdmin)
                {
                    var messageBytes = Encoding.UTF8.GetBytes(request.data);
                    adminMess.Send(messageBytes, SocketFlags.None);
                }
            }
        }
        catch (System.Net.Sockets.SocketException)
        {

        }
        catch (System.Text.Json.JsonException)
        {

        }
    }
    if (!client.Connected)
    {
        activeClient = false;
        MainMethod();
    }
}
//обработать System.Text.Json.JsonException(при закрытии приложения админа и клиента)
void Thread2(Socket admin)
{
    Console.WriteLine(String.Format("Подключено устройств: {0}",q));
    try
    {
        while (true)
        {
            var buffer = new byte[1_1000];
            var received = admin.Receive(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);
            Request request = JsonSerializer.Deserialize<Request>(response);
            switch (request.command)
            {
                case "entry":
                    string message;
                    bool exist;
                    string login = JsonSerializer.Deserialize<List<string>>(request.data)[0];
                    (message, exist) = Search.LoginInAdmin(Serialization.AdminIn<Admin>(), login, JsonSerializer.Deserialize<List<string>>(request.data)[1]);
                    List<string> mess = new List<string>() { message, JsonSerializer.Serialize(exist) };
                    Response resp = new Response(JsonSerializer.Serialize(mess), "null");
                    var messagBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(resp));
                    admin.Send(messagBytes, SocketFlags.None);
                    break;
                case "allClients":
                    List<User> users = Serialization.Input<User>();
                    resp = new Response("", JsonSerializer.Serialize(users));
                    messagBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(resp));
                    admin.Send(messagBytes, SocketFlags.None);
                    Serialization.Output(users);
                    break;
                case "ClientHistory":
                    users = Serialization.Input<User>();
                    foreach (User us in users)
                    {
                        us.FromServToHistory();
                    }
                    User user = Search.InfoForAdmin(users, Int32.Parse(request.data));
                    if (user.History != null && user.Services != null)
                    {
                        string[] data = { JsonSerializer.Serialize(user.History), JsonSerializer.Serialize(user.Services) };
                        resp = new Response("true", JsonSerializer.Serialize(data));
                        messagBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(resp));
                        admin.Send(messagBytes, SocketFlags.None);
                    }
                    else
                    {
                        resp = new Response("false", "");
                        messagBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(resp));
                        admin.Send(messagBytes, SocketFlags.None);
                    }
                    Serialization.Output(users);
                    break;
                case "PriceOut":
                    Dictionary<string, int> dic = Serialization.PriceOut();
                    List<string> list = Serialization.AllServicesOut();
                    List<string> allData = new List<string>() { JsonSerializer.Serialize(dic), JsonSerializer.Serialize(list) };
                    Response re = new Response("", JsonSerializer.Serialize(allData));
                    var messBy = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(re));
                    admin.Send(messBy, SocketFlags.None);
                    break;
                case "PriceIn":
                    dic = JsonSerializer.Deserialize<Dictionary<string, int>>(request.data);
                    //list = JsonSerializer.Deserialize<List<string>>(JsonSerializer.Deserialize<List<string>>(request.data)[1]);
                    Serialization.PriceIn(dic);
                    //Serialization.AllServicesIn(list);
                    break;
                case "AllServicesOut":
                    dic = Serialization.PriceOut();
                    list = Serialization.AllServicesOut();
                    allData = new List<string>() { JsonSerializer.Serialize(dic), JsonSerializer.Serialize(list) };
                    re = new Response("", JsonSerializer.Serialize(allData));
                    messBy = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(re));
                    admin.Send(messBy, SocketFlags.None);
                    break;
                case "AllServicesIn":
                    dic = JsonSerializer.Deserialize<Dictionary<string, int>>(JsonSerializer.Deserialize<List<string>>(request.data)[0]);
                    list = JsonSerializer.Deserialize<List<string>>(JsonSerializer.Deserialize<List<string>>(request.data)[1]);
                    Serialization.PriceIn(dic);
                    Serialization.AllServicesIn(list);
                    break;
                case "AllMessagesForAdmin":
                    Dictionary<string, List<string>> messageHistory = Serialization.AllMassegesOut();
                    resp = new Response("yes", JsonSerializer.Serialize(messageHistory));
                    var messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(resp));
                    admin.Send(messageBytes, SocketFlags.None);
                    break;

            }
        }
    }
    catch (System.Text.Json.JsonException)
    {
        Console.WriteLine("Admin отключился");
    }
}


void Thread1(Socket client)
{
    Console.WriteLine(String.Format("Подключено устройств: {0}",q));
    try
    {
        while (true)
        {

            var buffer = new byte[1_024];
            var received = client.Receive(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);
            Request request = JsonSerializer.Deserialize<Request>(response);
            switch (request.command)
            {
                case "registration":
                    User userReg = JsonSerializer.Deserialize<User>(request.data);
                    userReg.IdCount(Serialization.Input<User>());
                    if (!Search.Repetitions(Serialization.Input<User>(), userReg.Login))
                    {
                        bool correct = true;
                        List<ValidationResult> result;
                        (correct, result) = userReg.Validation();
                        if (!correct)
                        {
                            string problem = "";
                            foreach (var error in result)
                            {
                                problem = problem + error.ErrorMessage;
                            }
                            Response respon = new Response("no", problem);
                            var messBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(respon));
                            client.Send(messBytes, SocketFlags.None);
                        }
                        else
                        {
                            List<User> profUser = new List<User>();
                            profUser.Add(userReg);
                            Serialization.UserOut(profUser);
                            List<User> usersReg = Serialization.Input<User>();
                            usersReg.Add(userReg);
                            Dictionary<string, List<string>> allMessages = Serialization.AllMassegesOut();
                            allMessages.Add(userReg.Login, null);
                            Serialization.AllMassegesIn(allMessages);
                            Serialization.Output(usersReg);
                            Response respon = new Response("yes", "");
                            var messBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(respon));
                            client.Send(messBytes, SocketFlags.None);
                        }
                    }
                    else
                    {
                        Response respon = new Response("no", "Пользователь с таким логином уже существует");
                        var messBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(respon));
                        client.Send(messBytes, SocketFlags.None);
                    }
                    break;
                case "entry":
                    string message;
                    bool exist;
                    string login = JsonSerializer.Deserialize<List<string>>(request.data)[0];
                    (message, exist) = Search.LoginInUser(Serialization.Input<User>(), login, JsonSerializer.Deserialize<List<string>>(request.data)[1]);
                    List<string> mess = new List<string>() { message, JsonSerializer.Serialize(exist) };
                    Response resp = new Response(JsonSerializer.Serialize(mess), "null");
                    if (exist)
                    {
                        List<User> user = new List<User>();
                        user.Add(Search.Profile(Serialization.Input<User>(), login));
                        Serialization.UserOut(user);
                    }
                    var messagBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(resp));
                    client.Send(messagBytes, SocketFlags.None);
                    break;
                case "profile":
                    List<User> userProf = Serialization.UserIn<User>();
                    string listUser = JsonSerializer.Serialize(userProf);
                    var messageBytes = Encoding.UTF8.GetBytes(listUser);
                    client.Send(messageBytes, SocketFlags.None);
                    break;
                case "dialogForClient":
                    string userLogin = Serialization.UserIn<User>()[0].Login;
                    List<string> messageHistory = Serialization.AllMassegesOut()[userLogin];
                    if (messageHistory != null) resp = new Response("yes", JsonSerializer.Serialize(messageHistory));
                    else resp = new Response("no", "");
                    messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(resp));
                    client.Send(messageBytes, SocketFlags.None);
                    break;
                case "myService":
                    List<User> userProfile = Serialization.UserIn<User>();
                    if (userProfile[0].Services == null)
                    {
                        message = "no";
                    }
                    else
                    {
                        message = "yes";
                        userProfile[0].FromServToHistory();
                    }
                    Response res = new Response(message, JsonSerializer.Serialize(userProfile[0].Services));
                    messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(res));
                    client.Send(messageBytes, SocketFlags.None);
                    break;
                case "balance":
                    List<User> userProff = Serialization.UserIn<User>();
                    List<User> users = Serialization.Input<User>();
                    userProff[0].Balance = userProff[0].Balance + Int32.Parse(request.data);
                    users[Search.Profile(users, userProff[0].Login).Id - 1].Balance = userProff[0].Balance;
                    Response responss = new Response("ok", userProff[0].Balance.ToString());
                    Serialization.UserOut(userProff);
                    Serialization.Output(users);
                    messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(responss));
                    client.Send(messageBytes, SocketFlags.None);
                    break;
                case "history":
                    userProfile = Serialization.UserIn<User>();
                    string data;
                    if (userProfile[0].Services == null)
                    {
                        message = "no";
                        data = "no";
                    }
                    else
                    {
                        userProfile[0].FromServToHistory();
                        if (userProfile[0].History.Count == 0) { message = "no"; data = "no"; }
                        else { message = "yes"; data = JsonSerializer.Serialize(userProfile[0].History); }
                    }
                    Response respo = new Response(message, data);
                    messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(respo));
                    client.Send(messageBytes, SocketFlags.None);
                    break;
                case "makeService":
                    List<string> infServ = JsonSerializer.Deserialize<List<string>>(request.data);
                    string[] dataServ = User.DataShifr(infServ[0]);
                    bool corect;
                    string messag;
                    List<Service> services = new List<Service>();
                    userProf = Serialization.UserIn<User>();
                    (corect, message) = Search.CorrectDate(dataServ);
                    if (infServ[0] == "" | infServ[1] == "" | infServ[2] == "")
                    {
                        messag = "no";
                        data = "Одно из полей не заполнено!";
                    }
                    else
                    {
                        int resu = 0;
                        bool result = Int32.TryParse(infServ[2], out resu);
                        if (result)
                        {
                            if (corect)
                            {
                                Service service = new Service()
                                {
                                    Name = infServ[1],
                                    Price = Int32.Parse(infServ[2]),
                                    Date = infServ[0],
                                };
                                if (userProf[0].Services != null)
                                {
                                    services = userProf[0].Services;
                                }
                                if ((userProf[0].Balance - service.Price) < 0)
                                {
                                    data = "Недостаточно средств. Пополните баланс!";
                                    messag = "no";
                                }
                                else
                                {
                                    services.Add(service);
                                    data = $"Вы записались на услугу: {service.Name} на {service.Date}";
                                    userProf[0].Balance -= service.Price;
                                    userProf[0].Services = services;
                                    messag = "yes";
                                    users = Serialization.Input<User>();
                                    users[Search.Profile(users, userProf[0].Login).Id - 1].Services = userProf[0].Services;
                                    users[Search.Profile(users, userProf[0].Login).Id - 1].Balance = userProf[0].Balance;
                                    Serialization.Output<User>(users);
                                    Serialization.UserOut(userProf);
                                }
                            }
                            else
                            {
                                data = message;
                                messag = "no";
                            }
                        }
                        else { data = "Некорректные данные"; messag = "no"; }
                    }
                    Response reso = new Response(messag, data);
                    messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(reso));
                    client.Send(messageBytes, SocketFlags.None);
                    break;
                case "PriceOut":
                    Dictionary<string, int> dic = Serialization.PriceOut();
                    List<string> list = Serialization.AllServicesOut();
                    List<string> allData = new List<string>() { JsonSerializer.Serialize(dic), JsonSerializer.Serialize(list) };
                    Response re = new Response("", JsonSerializer.Serialize(allData));
                    var messBy = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(re));
                    client.Send(messBy, SocketFlags.None);
                    break;
            }
            continue;

        }
    }
    catch (System.Text.Json.JsonException)
    {
        Console.WriteLine("Client отключился");
    }
}
