using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Vems_Bot
{
    class Program
    {
        private static string token { get; set; } = "2065215367:AAHxs51AowRJAqefe3tvV7d5jn5nsC_-xDc";
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
            else
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Я так не умею");
            }
        }
    }
}