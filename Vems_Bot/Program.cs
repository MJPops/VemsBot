using HelloApp;
using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Vems_Bot.Models;


namespace Vems_Bot
{
    class Program
    {
        private static string token { get; set; } = "Token";
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
                    replyMarkup: Button.CoursesStartBack("Языки и ООП"));
            }
            else if (e.CallbackQuery.Data == "ООП")
            {
                await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.programmingOOP,
                    replyMarkup: Button.CoursesStartBack("Языки и ООП"));
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
                await client.SendTextMessageAsync(message.Chat.Id, Messages.aboutUs, replyMarkup: Button.GoToStart());
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

            else if (e.CallbackQuery.Data == "users")
            {
                using (ApplicationContext dataBase = new ApplicationContext())
                {
                    var users = dataBase.Users.ToList();

                    if (users.Any())
                    {
                        await client.SendTextMessageAsync(message.Chat.Id, "Пользователи:");
                        foreach (VemsUser user in users)
                        {
                            user.name ??= "Безымянный";
                            await client.SendTextMessageAsync(message.Chat.Id, $"{user.name}:", replyMarkup: Button.UsersMenu(user.id));
                        }
                    }
                    else
                    {
                        await client.SendTextMessageAsync(message.Chat.Id, "Пользователи не добавлены");
                    }
                }
            }

            try
            {
                if (e.CallbackQuery.Data.Substring(0, 2) == "id")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users.ToList()
                                           where user.id == e.CallbackQuery.Data.Substring(2)
                                           select user;

                        foreach (VemsUser user in selectedUser)
                        {
                            await client.EditMessageTextAsync(message.Chat.Id,
                                message.MessageId,
                                $"▫ id: {user.id}\n\n" +
                                $"▫ Имя: {user.name}\n\n" +
                                $"▫ Курс: {user.course}\n\n" +
                                $"▫ Материалы: {user.documentLink}\n\n" +
                                $"▫ Описание: {user.description}\n\n" +
                                $"▫ Заметка: {user.note}",
                                replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Button.InChange(user.id));
                        }
                    }
                }
                else if (e.CallbackQuery.Data.Substring(0, 6) == "change")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users.ToList()
                                           where user.id == e.CallbackQuery.Data.Substring(6)
                                           select user;

                        foreach (VemsUser user in selectedUser)
                        {
                            await client.EditMessageTextAsync(message.Chat.Id,
                                message.MessageId,
                                $"▫ id: {user.id}\n\n" +
                                $"▫ Имя: {user.name}\n\n" +
                                $"▫ Курс: {user.course}\n\n" +
                                $"▫ Материалы: {user.documentLink}\n\n" +
                                $"▫ Описание: {user.description}\n\n" +
                                $"▫ Заметка: {user.note}\n\n" +
                                $"Выберите пункт изменения/добавления",
                                replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Button.InChange(user.id));
                        }
                    }
                }
                else if (e.CallbackQuery.Data.Substring(0, 4) == "name")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users
                                           where user.id == e.CallbackQuery.Data.Substring(4)
                                           select user;

                        if (!selectedUser.Any())
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, "Такого пользователя не существует");
                        }
                        else
                        {
                            foreach (VemsUser user in selectedUser)
                            {
                                await client.EditMessageTextAsync(message.Chat.Id,
                                    message.MessageId,
                                    $"▫ id: {user.id}\n\n" +
                                    $"▫ Имя: Изменяется\n\n" +
                                    $"▫ Курс: {user.course}\n\n" +
                                    $"▫ Материалы: {user.documentLink}\n\n" +
                                    $"▫ Описание: {user.description}\n\n" +
                                    $"▫ Заметка: {user.note}\n\n" +
                                    $"Выберите пункт для изменения/добавления",
                                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Button.InChange(user.id));

                                await client.SendTextMessageAsync(message.Chat.Id, "Введите имя");
                                VemsUser.parametrSetingStatus = "name";
                                VemsUser.userToChange = e.CallbackQuery.Data.Substring(4);
                            }
                        }
                    }
                }
                else if (e.CallbackQuery.Data.Substring(0, 4) == "cour")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users
                                           where user.id == e.CallbackQuery.Data.Substring(4)
                                           select user;

                        if (!selectedUser.Any())
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, "Такого пользователя не существует");
                        }
                        else
                        {
                            foreach (VemsUser user in selectedUser)
                            {
                                await client.EditMessageTextAsync(message.Chat.Id,
                                    message.MessageId,
                                    $"▫ id: {user.id}\n\n" +
                                    $"▫ Имя: {user.name}\n\n" +
                                    $"▫ Курс: Изменяется\n\n" +
                                    $"▫ Материалы: {user.documentLink}\n\n" +
                                    $"▫ Описание: {user.description}\n\n" +
                                    $"▫ Заметка: {user.note}\n\n" +
                                    $"Выберите пункт для изменения/добавления",
                                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Button.InChange(user.id));

                                await client.SendTextMessageAsync(message.Chat.Id, "Введите название курса");
                                VemsUser.parametrSetingStatus = "cour";
                                VemsUser.userToChange = e.CallbackQuery.Data.Substring(4);
                            }
                        }
                    }
                }
                else if (e.CallbackQuery.Data.Substring(0, 4) == "link")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users
                                           where user.id == e.CallbackQuery.Data.Substring(4)
                                           select user;

                        if (!selectedUser.Any())
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, "Такого пользователя не существует");
                        }
                        else
                        {
                            foreach (VemsUser user in selectedUser)
                            {
                                await client.EditMessageTextAsync(message.Chat.Id,
                                    message.MessageId,
                                    $"▫ id: {user.id}\n\n" +
                                    $"▫ Имя: {user.name}\n\n" +
                                    $"▫ Курс: {user.course}\n\n" +
                                    $"▫ Материалы: Изменяется\n\n" +
                                    $"▫ Описание: {user.description}\n\n" +
                                    $"▫ Заметка: {user.note}\n\n" +
                                    $"Выберите пункт для изменения/добавления",
                                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Button.InChange(user.id));

                                await client.SendTextMessageAsync(message.Chat.Id, "Введите ссылку");
                                VemsUser.parametrSetingStatus = "link";
                                VemsUser.userToChange = e.CallbackQuery.Data.Substring(4);
                            }
                        }
                    }
                }
                else if (e.CallbackQuery.Data.Substring(0, 4) == "desc")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users
                                           where user.id == e.CallbackQuery.Data.Substring(4)
                                           select user;

                        if (!selectedUser.Any())
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, "Такого пользователя не существует");
                        }
                        else
                        {
                            foreach (VemsUser user in selectedUser)
                            {
                                await client.EditMessageTextAsync(message.Chat.Id,
                                    message.MessageId,
                                    $"▫ id: {user.id}\n\n" +
                                    $"▫ Имя: {user.name}\n\n" +
                                    $"▫ Курс: {user.course}\n\n" +
                                    $"▫ Материалы: {user.documentLink}\n\n" +
                                    $"▫ Описание: Изменяется\n\n" +
                                    $"▫ Заметка: {user.note}\n\n" +
                                    $"Выберите пункт для изменения/добавления",
                                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Button.InChange(user.id));

                                await client.SendTextMessageAsync(message.Chat.Id, "Введите описание");
                                VemsUser.parametrSetingStatus = "desc";
                                VemsUser.userToChange = e.CallbackQuery.Data.Substring(4);
                            }
                        }
                    }
                }
                else if (e.CallbackQuery.Data.Substring(0, 4) == "note")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users
                                           where user.id == e.CallbackQuery.Data.Substring(4)
                                           select user;

                        if (!selectedUser.Any())
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, "Такого пользователя не существует");
                        }
                        else
                        {
                            foreach (VemsUser user in selectedUser)
                            {
                                user.note = "";
                                await client.EditMessageTextAsync(message.Chat.Id,
                                    message.MessageId,
                                    $"▫ id: {user.id}\n\n" +
                                    $"▫ Имя: {user.name}\n\n" +
                                    $"▫ Курс: {user.course}\n\n" +
                                    $"▫ Материалы: {user.documentLink}\n\n" +
                                    $"▫ Описание: {user.description}\n\n" +
                                    $"▫ Заметка: Изменяется\n\n" +
                                    $"Выберите пункт для изменения/добавления",
                                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Button.InChange(user.id));

                                await client.SendTextMessageAsync(message.Chat.Id, "Введите заметку");
                                VemsUser.parametrSetingStatus = "note";
                                VemsUser.userToChange = e.CallbackQuery.Data.Substring(4);
                            }
                            await dataBase.SaveChangesAsync();
                        }
                    }
                }
                else if (e.CallbackQuery.Data.Substring(0, 3) == "del")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users
                                           where user.id == e.CallbackQuery.Data.Substring(3)
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
                            await dataBase.SaveChangesAsync();
                            await client.EditMessageTextAsync(message.Chat.Id,
                                message.MessageId,
                                "Пользователь удален");
                        }
                    }
                }
                else if (e.CallbackQuery.Data.Substring(0, 4) == "user")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users.ToList()
                                           where user.id == e.CallbackQuery.Data.Substring(4)
                                           select user;

                        foreach (VemsUser user in selectedUser)
                        {
                            user.name ??= "Безымянный";

                            await client.EditMessageTextAsync(message.Chat.Id,
                                message.MessageId,
                                $"{user.name}:",
                                replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Button.UsersMenu(user.id));
                        }
                    }
                }
            }
            catch
            {

            }
        }

        [Obsolete]
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            bool unknownMessage = false;
            string pasword = "2143";

            if (message.Text != null)
            {
                Console.WriteLine($"Пришло сообщение: {message.Text}");
            }

            if (VemsUser.parametrSetingStatus != null)
            {
                if (VemsUser.parametrSetingStatus == "name")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users
                                           where user.id == VemsUser.userToChange
                                           select user;

                        foreach (VemsUser user in selectedUser)
                        {
                            user.name = message.Text;
                        }
                        await dataBase.SaveChangesAsync();
                        await client.SendTextMessageAsync(message.Chat.Id, "Чтобы увидеть изменение - сверните и разверните или " +
                            "снова вызовите список учеников", replyMarkup:Button.ToUsers());
                    }
                }
                else if (VemsUser.parametrSetingStatus == "cour")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users
                                           where user.id == VemsUser.userToChange
                                           select user;

                        foreach (VemsUser user in selectedUser)
                        {
                            user.course = message.Text;
                        }
                        await dataBase.SaveChangesAsync();
                        await client.SendTextMessageAsync(message.Chat.Id, "Чтобы увидеть изменение - сверните и разверните или " +
                            "снова вызовите список учеников", replyMarkup: Button.ToUsers());
                    }
                }
                else if (VemsUser.parametrSetingStatus == "link")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users
                                           where user.id == VemsUser.userToChange
                                           select user;

                        foreach (VemsUser user in selectedUser)
                        {
                            user.documentLink = message.Text;
                        }
                        await dataBase.SaveChangesAsync();
                        await client.SendTextMessageAsync(message.Chat.Id, "Чтобы увидеть изменение - сверните и разверните или " +
                            "снова вызовите список учеников", replyMarkup: Button.ToUsers());
                    }
                }
                else if (VemsUser.parametrSetingStatus == "desc")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users
                                           where user.id == VemsUser.userToChange
                                           select user;

                        foreach (VemsUser user in selectedUser)
                        {
                            user.description = message.Text;
                        }
                        await dataBase.SaveChangesAsync();
                        await client.SendTextMessageAsync(message.Chat.Id, "Чтобы увидеть изменение - сверните и разверните или " +
                            "снова вызовите список учеников", replyMarkup: Button.ToUsers());
                    }
                }
                else if (VemsUser.parametrSetingStatus == "note")
                {
                    using (ApplicationContext dataBase = new ApplicationContext())
                    {
                        var selectedUser = from user in dataBase.Users
                                           where user.id == VemsUser.userToChange
                                           select user;

                        foreach (VemsUser user in selectedUser)
                        {
                            user.note = message.Text;
                        }
                        await dataBase.SaveChangesAsync();
                        await client.SendTextMessageAsync(message.Chat.Id, "Чтобы увидеть изменение - сверните и разверните или " +
                            "снова вызовите список учеников", replyMarkup: Button.ToUsers());
                    }
                }

                VemsUser.userToChange = null;
                VemsUser.parametrSetingStatus = null;
            }

            else if (message.Text == pasword + "users")
            {
                using (ApplicationContext dataBase = new ApplicationContext())
                {
                    var users = dataBase.Users.ToList();

                    if (users.Any())
                    {
                        await client.SendTextMessageAsync(message.Chat.Id, "Пользователи:");
                        foreach (VemsUser user in users)
                        {
                            user.name ??= "Безымянный";
                            await client.SendTextMessageAsync(message.Chat.Id, $"{user.name}:", replyMarkup: Button.UsersMenu(user.id));
                        }
                    }
                    else
                    {
                        await client.SendTextMessageAsync(message.Chat.Id, "Пользователи не добавлены");
                    }
                }
            }
            else if (message.Text == "/start")
            {
                await client.SendTextMessageAsync(message.Chat.Id, Messages.start, replyMarkup: Button.Start());
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
                        bool idWorked = false;

                        await client.SendTextMessageAsync(message.Chat.Id, $"Пользователь с id{message.Text.Substring(2)}");

                        var selectedUser = from user in dataBase.Users
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
                            bool notError = true;

                            if (message.Text.Substring(7).Length != 4)
                            {
                                notError = false;
                                await client.SendTextMessageAsync(message.Chat.Id, "id должен содержать ровно 4 символа");
                            }
                            else
                            {
                                var selectedUser = from user in dataBase.Users
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
                                dataBase.Add(newUser);
                                await dataBase.SaveChangesAsync();
                                await client.SendTextMessageAsync(message.Chat.Id, "Id зарегистрирован");
                            }
                        }
                    }
                    else if (message.Text.Length >= 8)
                    {
                        if (message.Text.Substring(0, 8) == pasword + "user")
                        {
                            using (ApplicationContext dataBase = new ApplicationContext())
                            {
                                int numberOfCharactersID = message.Text.Length - 8;

                                if (numberOfCharactersID < 5 && numberOfCharactersID > 0)
                                {
                                    var selectedUser = from user in dataBase.Users
                                                       where message.Text.Substring(8, numberOfCharactersID) ==
                                                       user.id.Substring(0, numberOfCharactersID)
                                                       select user;

                                    if (selectedUser.Any())
                                    {
                                        await client.SendTextMessageAsync(message.Chat.Id, "Пользователи:");
                                        foreach (VemsUser user in selectedUser)
                                        {
                                            user.name ??= "Безымянный";

                                            await client.SendTextMessageAsync(message.Chat.Id,
                                                $"{user.name}:",
                                                replyMarkup:Button.UsersMenu(user.id));
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