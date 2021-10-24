using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Vems_Bot
{
    class Button
    {
        public static IReplyMarkup Start()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Курсы" }, new KeyboardButton { Text="О Нас"},
                    new KeyboardButton{Text="Преподаватели"} },

                    new List<KeyboardButton>{new KeyboardButton { Text = "/start"} },

                    new List<KeyboardButton>{new KeyboardButton { Text="Контакты"}, new KeyboardButton { Text="Анкета для регистрации"} }
                }
            };
        }
        public static IReplyMarkup HededCourses()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Веб направление" }, 
                        new KeyboardButton { Text="Языки и ООП"},
                        new KeyboardButton{Text="Репетиторство"} },

                    new List<KeyboardButton>{new KeyboardButton { Text = "/start", } }
                }
            };
        }
        public static IReplyMarkup WebCourses()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Веб-Дизайн" },
                        new KeyboardButton { Text="Java- и  TypeScript"},
                        new KeyboardButton{Text="JavaScript и фреймворки"} },

                    new List<KeyboardButton>{ new KeyboardButton { Text = "Курсы", },
                        new KeyboardButton { Text = "/start", } }
                }
            };
        }
        public static IReplyMarkup ProgrammingCourses()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Основы программирования" },
                        new KeyboardButton { Text="ООП"}},

                    new List<KeyboardButton>{ new KeyboardButton { Text = "Курсы", },
                        new KeyboardButton { Text = "/start", } }
                }
            };
        }
        public static IReplyMarkup TutoringCourses()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    //new List<KeyboardButton>{ new KeyboardButton { Text = "Математика" },
                    //    new KeyboardButton { Text="Информатика"},
                    //    new KeyboardButton{Text="Английский"} },

                    new List<KeyboardButton>{ new KeyboardButton { Text = "Курсы", },
                        new KeyboardButton { Text = "/start", } }
                }
            };
        }
        public static IReplyMarkup Form()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithUrl(text:"Анкета", url: Links.form)
            });;
        }
        public static IReplyMarkup Contacts()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>{ InlineKeyboardButton.WithUrl(text: "Instagram", url: Links.instagram) },
                    new List<InlineKeyboardButton>{ InlineKeyboardButton.WithUrl(text:"WhatsApp", url: Links.whatsApp) },
                    new List<InlineKeyboardButton>{ InlineKeyboardButton.WithUrl(text:"Telegram", url: Links.telegram) }
            });
            ;
        }
    }
}
