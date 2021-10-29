using HelloApp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Vems_Bot.Models;


namespace Vems_Bot
{
    class Program
    {
        private static string token { get; set; } = "Токен";
        private static TelegramBotClient client;

        
        [Obsolete]
        static void Main(string[] args)
        {
            try
            {
                client = new TelegramBotClient(token);
                client.StartReceiving();
                client.OnMessage += OnMessageHandler;
                client.OnCallbackQuery += OnCallbackQweryHandlerAsync;
                Console.ReadLine();
                client.StopReceiving();
            }
            catch
            {
                Console.WriteLine("ERROR");
            }
        }

        [Obsolete]
        private static async void OnCallbackQweryHandlerAsync(object sender, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;

            if (e.CallbackQuery.Data != null)
            {
                Console.WriteLine($"Нажата кнопка: {e.CallbackQuery.Data}");
            }
            if (e.CallbackQuery.Data == "/start")
            {
                await client.SendTextMessageAsync(message.Chat.Id, Messages.start, replyMarkup: Button.Start());
            }

            if (e.CallbackQuery.Data == "Курсы")
            {
                await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.coursesHeded,
                    replyMarkup: Button.HededCourses());
            }

            else if (e.CallbackQuery.Data == "Веб направление")
            {
                await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.coursesWeb,
                    replyMarkup: Button.WebCourses());
            }
            else if (e.CallbackQuery.Data == "Веб-Дизайн")
            {
                await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.webDesign,
                    replyMarkup: Button.CoursesStartBack("Веб направление"));
            }
            else if (e.CallbackQuery.Data == "Java- и  TypeScript")
            {
                await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.webDevelop,
                    replyMarkup: Button.CoursesStartBack("Веб направление"));
            }
            else if (e.CallbackQuery.Data == "JavaScript и фреймворки")
            {
                await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.webFramework,
                    replyMarkup: Button.CoursesStartBack("Веб направление"));
            }

            else if (e.CallbackQuery.Data == "Языки и ООП")
            {
                await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.coursesProgramming,
                    replyMarkup: Button.ProgrammingCourses());
            }
            else if (e.CallbackQuery.Data == "Основы программирования")
            {
                await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.programmingBasics,
                    replyMarkup: Button.CoursesStartBack("Веб направление"));
            }
            else if (e.CallbackQuery.Data == "ООП")
            {
                await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.programmingOOP,
                    replyMarkup: Button.CoursesStartBack("Веб направление"));
            }

            else if (e.CallbackQuery.Data == "Репетиторство")
            {
                await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.coursesTutoring,
                    replyMarkup: Button.CoursesStart());
            }

            else if (e.CallbackQuery.Data == "О Нас")
            {
                await client.SendTextMessageAsync(message.Chat.Id, Messages.aboutUs, replyMarkup:Button.GoToStart());
            }
            else if (e.CallbackQuery.Data == "Преподаватели")
            {
                await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.matvey);

                await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.sasha,
                    replyMarkup: Button.GoToStart());
            }
            else if (e.CallbackQuery.Data == "Анкета для регистрации")
            {
                await client.SendTextMessageAsync(message.Chat.Id, Messages.form, replyMarkup: Button.Form());
            }
            else if (e.CallbackQuery.Data == "Контакты")
            {
                await client.SendTextMessageAsync(message.Chat.Id, Messages.contscts, replyMarkup: Button.Contacts());

                await client.SendContactAsync(
                    message.Chat.Id,
                    phoneNumber: "79609044065",
                    firstName: "Ева,",
                    lastName: "менеджер",
                    replyMarkup: Button.GoToStart());
            }
        }

        [Obsolete]
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            bool unknownMessage = false;
            string pasword = "2143";

            if (message.Text == pasword + "users")
            {
                using (ApplicationContext dataBase = new ApplicationContext())
                {
                    var users = dataBase.Users.ToList();

                    if (users.Any())
                    {
                        await client.SendTextMessageAsync(message.Chat.Id, "Пользователи:");
                        foreach (VemsUser user in users)
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, $"id{user.id} -- {user.name}");
                        }
                    }
                    else
                    {
                        await client.SendTextMessageAsync(message.Chat.Id, "Пользователи не добавлены");
                    }
                }
            }
            else if (message.Text == "Служебные команды")
            {
                await client.SendTextMessageAsync(message.Chat.Id, Messages.registrationProcedure);
            }
            else if (message.Text.Length >= 2)
            {
                if (message.Text.Substring(0, 2) == "id")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var users = dataBase.Users.ToList();
                        bool idWorked = false;

                        await client.SendTextMessageAsync(message.Chat.Id, $"Пользователь с id{message.Text.Substring(2)}");

                        var selectedUser = from user in users
                                           where user.id == message.Text.Substring(2)
                                           select user;

                        foreach (var user in selectedUser)
                        {
                            if (user.name == null && user.course == null && user.documentLink == null)
                            {
                                await client.SendTextMessageAsync(message.Chat.Id, $"▫ Для данного id пока не добавлены данные. " +
                                    $"Если ничего не появится, просьба уточнить у преподавателя.");
                            }
                            if (user.name != null && user.course == null && user.documentLink == null)
                            {
                                await client.SendTextMessageAsync(message.Chat.Id, $"▫ Здравствуйте, {user.name}, на данный момент " +
                                    $"информация по вашему курсу не добавлена.");
                            }
                            if(user.name != null && user.course != null && user.documentLink == null)
                            {
                                await client.SendTextMessageAsync(message.Chat.Id, $"▫ Здравствуйте, {user.name}, на данный момент вам " +
                                   $"не доступны материалы по курсу {user.course}");
                            }
                            if (user.documentLink != null && user.course != null && user.name != null)
                            {
                                await client.SendTextMessageAsync(message.Chat.Id, $"▫ Здравствуйте, {user.name}, на данный момент вам " +
                                   $"не доступны материалы по курсу {user.course}👇",
                                   replyMarkup: Button.DocumentLink(user.documentLink));
                            }
                            if (user.documentLink != null && user.course == null && user.name != null)
                            {
                                await client.SendTextMessageAsync(message.Chat.Id, $"▫ Здравствуйте, {user.name}, на данный момент вам " +
                                   $"доступны следующие материалы👇",
                                   replyMarkup: Button.DocumentLink(user.documentLink));
                            }
                            if (user.description != null)
                            {
                                await client.SendTextMessageAsync(message.Chat.Id, $"{user.description}");
                            }
                            idWorked = true;
                        }
                        if (!idWorked)
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, "▫ Такого пользователь не существует\n\n" +
                                "▫ Проверьте корректность id или уточните у преподавателя, добавил ли он вас");
                        }
                    }
                }
                else if (message.Text.Length >= 7)
                {
                    if (message.Text.Substring(0, 7) == pasword + "add")
                    {
                        using (ApplicationContext dataBase = new ApplicationContext())
                        {
                            VemsUser newUser = new VemsUser { id = message.Text.Substring(7) };
                            var users = dataBase.Users.ToList();
                            bool notError = true;

                            if (message.Text.Substring(7).Length != 4)
                            {
                                notError = false;
                                await client.SendTextMessageAsync(message.Chat.Id, "id должен содержать ровно 4 символа");
                            }
                            else
                            {
                                var selectedUser = from user in users
                                                   where user.id == message.Text.Substring(7)
                                                   select user;

                                if (selectedUser.Any())
                                {
                                    notError = false;
                                    await client.SendTextMessageAsync(message.Chat.Id, "Такой id уже существует");
                                }
                            }

                            if (notError)
                            {
                                dataBase.Users.Add(newUser);
                                dataBase.SaveChanges();
                                await client.SendTextMessageAsync(message.Chat.Id, "Id зарегистрирован");
                            }
                        }
                    }
                    else if (message.Text.Substring(0, 7) == pasword + "del")
                    {
                        using (ApplicationContext dataBase = new ApplicationContext())
                        {
                            var users = dataBase.Users.ToList();

                            var selectedUser = from user in users
                                               where user.id == message.Text.Substring(7)
                                               select user;

                            if (!selectedUser.Any())
                            {
                                await client.SendTextMessageAsync(message.Chat.Id, "Такого пользователя не существует");
                            }
                            else
                            {
                                foreach (VemsUser user in selectedUser)
                                {
                                    dataBase.Users.RemoveRange(user);
                                }
                                dataBase.SaveChanges();
                                await client.SendTextMessageAsync(message.Chat.Id, "Пользователь удален");
                            }
                        }
                    }
                    else if (message.Text.Length >= 8)
                    {
                        if (message.Text.Substring(0, 8) == pasword + "name")
                        {
                            using (ApplicationContext dataBase = new ApplicationContext())
                            {
                                var users = dataBase.Users.ToList();

                                var selectedUser = from user in users
                                                   where user.id == message.Text.Substring(8, 4)
                                                   select user;

                                if (!selectedUser.Any())
                                {
                                    await client.SendTextMessageAsync(message.Chat.Id, "id не обнаружен");
                                }
                                else
                                {
                                    foreach (VemsUser user in selectedUser)
                                    {
                                        user.name = message.Text.Substring(13);
                                    }
                                    await client.SendTextMessageAsync(message.Chat.Id, "Имя добавлено");
                                    dataBase.SaveChanges();
                                }
                            }
                        }
                        else if (message.Text.Substring(0, 8) == pasword + "cour")
                        {
                            using (ApplicationContext dataBase = new ApplicationContext())
                            {
                                var users = dataBase.Users.ToList();

                                var selectedUser = from user in users
                                                   where user.id == message.Text.Substring(8, 4)
                                                   select user;

                                if (!selectedUser.Any())
                                {
                                    await client.SendTextMessageAsync(message.Chat.Id, "id не обнаружен");
                                }
                                else
                                {
                                    foreach (VemsUser user in selectedUser)
                                    {
                                        user.course = message.Text.Substring(13);
                                    }
                                    await client.SendTextMessageAsync(message.Chat.Id, "Наименование курса добавлено");
                                    dataBase.SaveChanges();
                                }
                            }
                        }
                        else if (message.Text.Substring(0, 8) == pasword + "link")
                        {
                            using (ApplicationContext dataBase = new ApplicationContext())
                            {
                                var users = dataBase.Users.ToList();

                                var selectedUser = from user in users
                                                   where user.id == message.Text.Substring(8, 4)
                                                   select user;

                                if (!selectedUser.Any())
                                {
                                    await client.SendTextMessageAsync(message.Chat.Id, "id не обнаружен");
                                }
                                else
                                {
                                    foreach (VemsUser user in selectedUser)
                                    {
                                        user.documentLink = message.Text.Substring(13);
                                    }
                                    await client.SendTextMessageAsync(message.Chat.Id, "Ссылка добавлена");
                                    dataBase.SaveChanges();
                                }
                            }
                        }
                        else if (message.Text.Substring(0, 8) == pasword + "desc")
                        {
                            using (ApplicationContext dataBase = new ApplicationContext())
                            {
                                var users = dataBase.Users.ToList();

                                var selectedUser = from user in users
                                                   where user.id == message.Text.Substring(8, 4)
                                                   select user;

                                if (!selectedUser.Any())
                                {
                                    await client.SendTextMessageAsync(message.Chat.Id, "id не обнаружен");
                                }
                                else
                                {
                                    foreach (VemsUser user in selectedUser)
                                    {
                                        user.description = message.Text.Substring(13);
                                    }
                                    await client.SendTextMessageAsync(message.Chat.Id, "Описание добавлено");
                                    dataBase.SaveChanges();
                                }
                            }
                        }
                        else if (message.Text.Substring(0, 8) == pasword + "user")
                        {
                            using (ApplicationContext dataBase = new ApplicationContext())
                            {
                                var users = dataBase.Users.ToList();
                                int numberOfCharactersID = message.Text.Length - 8;

                                if (numberOfCharactersID < 5 && numberOfCharactersID > 0)
                                {
                                    var selectedUser = from user in users
                                                       where message.Text.Substring(8, numberOfCharactersID) ==
                                                       user.id.Substring(0, numberOfCharactersID)
                                                       select user;

                                    if (selectedUser.Any())
                                    {
                                        await client.SendTextMessageAsync(message.Chat.Id, "Пользователи:");
                                        foreach (VemsUser user in selectedUser)
                                        {
                                            await client.SendTextMessageAsync(message.Chat.Id, $"id{user.id} -- {user.name}");
                                        }
                                    }
                                    else
                                    {
                                        await client.SendTextMessageAsync(message.Chat.Id, "Таких пользователей не обнаружено");
                                    }
                                }
                            }
                        }
                        else
                        {
                            unknownMessage = true;
                        }
                    }
                    else
                    {
                        unknownMessage = true;
                    }
                }
                else
                {
                    unknownMessage = true;
                }
            }
            
            else
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Я так не умею");
            }

            if (unknownMessage)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Я так не умею");
            }
        }
    }
}