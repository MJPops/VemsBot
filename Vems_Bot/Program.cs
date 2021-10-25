using HelloApp;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Vems_Bot.Models;


namespace Vems_Bot
{
    class Program
    {
        private static string token { get; set; } = "Токен Сюда";
        private static TelegramBotClient client;

        
        [Obsolete]
        static void Main(string[] args)
        {
            try
            {
                client = new TelegramBotClient(token);
                client.StartReceiving();
                client.OnMessage += OnMessageHandler;
                Console.ReadLine();
                client.StopReceiving();
            }
            catch
            {
                Console.WriteLine("ERROR");
            }
        }

        [Obsolete]
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var message = e.Message;

            if(message.Text != null)
            {
                Console.WriteLine($"Пришло сообщение с текстом: {message.Text}");
            }
            
            if (message.Text == "/start")
            {
                await client.SendTextMessageAsync(message.Chat.Id, Messages.start, replyMarkup: Button.Start());
            }


            else if (message.Text == "Курсы")
            {
                var photo = await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.coursesHeded);

                await client.SendTextMessageAsync(message.Chat.Id, Messages.headCourses, replyMarkup: Button.HededCourses());
            }

            else if (message.Text == "Веб направление")
            {
                var photo = await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.coursesWeb,
                    replyMarkup:Button.WebCourses());
            }
            else if (message.Text == "Веб-Дизайн")
            {
                var photo = await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.webDesign);
            }
            else if (message.Text == "Java- и  TypeScript")
            {
                var photo = await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.webDevelop);
            }
            else if (message.Text == "JavaScript и фреймворки")
            {
                var photo = await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.webFramework);
            }

            else if (message.Text == "Языки и ООП")
            {
                var photo = await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.coursesProgramming,
                    replyMarkup: Button.ProgrammingCourses());
            }
            else if (message.Text == "Основы программирования")
            {
                var photo = await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.programmingBasics);
            }
            else if (message.Text == "ООП")
            {
                var photo = await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.programmingOOP);
            }

            else if (message.Text == "Репетиторство")
            {
                var photo = await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.coursesTutoring,
                    replyMarkup: Button.TutoringCourses());
            }


            else if (message.Text == "О Нас")
            {
                await client.SendTextMessageAsync(message.Chat.Id, Messages.aboutUs);
            }
            else if (message.Text == "Преподаватели")
            {
                var photo = await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.matvey);

                photo = await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: Links.sasha);
            }
            else if (message.Text == "Анкета для регистрации")
            {
                await client.SendTextMessageAsync(message.Chat.Id, Messages.form, replyMarkup: Button.Form());
            }
            else if (message.Text == "Контакты")
            {
                await client.SendTextMessageAsync(message.Chat.Id, Messages.contscts, replyMarkup: Button.Contacts());

                await client.SendContactAsync(
                    message.Chat.Id,
                    phoneNumber: "79609044065",
                    firstName: "Ева,",
                    lastName: "менеджер");
            }

            else if (message.Text == "4365users")
            {
                using (ApplicationContext dataBase = new ApplicationContext())
                {
                    var users = dataBase.Users.ToList();

                    await client.SendTextMessageAsync(message.Chat.Id, "Пользователи:");
                    foreach (VemsUser user in users)
                    {
                        await client.SendTextMessageAsync(message.Chat.Id, $"{user.name} -- id{user.id}");
                    }
                }
            }
            else if (message.Text == "Порядок регистрации")
            {
                await client.SendTextMessageAsync(message.Chat.Id, "▫ Пример: reg|id|Имя|Курс|Ссылка|Описание\n\n▫ id должен содержать 4 символа");
                await client.SendTextMessageAsync(message.Chat.Id, "▫ Шаблон: reg|||||");
            }
            else if (message.Text.Length >= 7)
            {
                try
                {
                    if (message.Text.Substring(0, 2) == "id")
                    {
                        using (ApplicationContext dataBase = new ApplicationContext())
                        {
                            var users = dataBase.Users.ToList();
                            int error = 0;

                            await client.SendTextMessageAsync(message.Chat.Id, $"Пользователи с id{message.Text.Substring(2)}");

                            foreach (VemsUser user in users)
                            {
                                if (user.id == message.Text.Substring(2))
                                {
                                    await client.SendTextMessageAsync(message.Chat.Id, $"▫ Здравствуйте, {user.name}, на данный момент вам " +
                                        $"доступны следующие документы по курсу {user.course}👇");
                                    await client.SendTextMessageAsync(message.Chat.Id, $"Ссылка: {user.documentLink}");
                                    await client.SendTextMessageAsync(message.Chat.Id, $"{user.description}");
                                    error = 1;
                                }
                            }
                            if (error == 0)
                            {
                                await client.SendTextMessageAsync(message.Chat.Id, "▫ Такого пользователь не существует\n\n" +
                                    "▫ Проверьте корректность id или уточните у преподавателя, добавил ли он вас");
                            }
                        }
                    }
                    else if (message.Text.Substring(0, 7) == "4365reg")
                    {
                        try
                        {
                            string[] registrateParametrs = message.Text.Split("|");
                            using (ApplicationContext dataBase = new ApplicationContext())
                            {
                                var users = dataBase.Users.ToList();
                                bool error = true;

                                if (registrateParametrs[1].Length != 4)
                                {
                                    error = false;
                                    await client.SendTextMessageAsync(message.Chat.Id, "id должен содержать ровно 4 символа");
                                }

                                else
                                {
                                    foreach (VemsUser user in users)
                                    {
                                        if (user.id == registrateParametrs[1])
                                        {
                                            error = false;
                                            await client.SendTextMessageAsync(message.Chat.Id, "Такой id уже существует");
                                        }
                                    }
                                }

                                if (error)
                                {
                                    VemsUser newUser = new VemsUser
                                    {
                                        id = registrateParametrs[1],
                                        name = registrateParametrs[2],
                                        course = registrateParametrs[3],
                                        documentLink = registrateParametrs[4],
                                        description = registrateParametrs[5]
                                    };
                                    dataBase.Users.Add(newUser);
                                    dataBase.SaveChanges();
                                    await client.SendTextMessageAsync(message.Chat.Id, "Пользователь зарегистрирован");
                                }
                            }
                        }
                        catch
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, "Ошибка ввода");
                        }
                    }
                    else if (message.Text.Substring(0, 7) == "4365del")
                    {
                        using (ApplicationContext dataBase = new ApplicationContext())
                        {
                            var users = dataBase.Users.ToList();
                            VemsUser delitedUser = new VemsUser();

                            foreach (VemsUser user in users)
                            {
                                if (user.id == message.Text.Substring(7))
                                {
                                    delitedUser = user;
                                }
                            }
                            dataBase.Users.RemoveRange(delitedUser);
                            dataBase.SaveChanges();
                            await client.SendTextMessageAsync(message.Chat.Id, "Пользователь удален");
                        }
                    }
                }
                catch (Exception)
                {
                    await client.SendTextMessageAsync(message.Chat.Id, "Такого id не существует");
                }
            }

            else
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Я так не умею");
            }
        }
    }
}