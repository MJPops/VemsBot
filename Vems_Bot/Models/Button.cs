using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;
using Vems_Bot.Models;

namespace Vems_Bot
{
    class Button
    {
        public static IReplyMarkup Start()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>{
                        InlineKeyboardButton.WithCallbackData(text: "Курсы", callbackData: "Курсы"),
                        InlineKeyboardButton.WithCallbackData(text: "О Нас", callbackData: "О Нас"),
                        InlineKeyboardButton.WithCallbackData(text: "Преподаватели", callbackData: "Преподаватели")},
                    new List<InlineKeyboardButton>{
                        InlineKeyboardButton.WithCallbackData(text: "Контакты", callbackData: "Контакты"),
                        InlineKeyboardButton.WithCallbackData(text: "Анкета для регистрации", callbackData: "Анкета для регистрации")}
            });
            ;
        }
        public static IReplyMarkup GoToStart()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "Основное меню", callbackData: "/start")
                }
            });
            ;
        }
        public static IReplyMarkup CoursesStart()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "Курсы", callbackData: "Курсы"),
                    InlineKeyboardButton.WithCallbackData(text: "Основное меню", callbackData: "/start")
                }
            });
            ;
        }
        public static IReplyMarkup CoursesStartBack(string backTo)
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "<<Назад", callbackData: backTo),
                    InlineKeyboardButton.WithCallbackData(text: "Курсы", callbackData: "Курсы")
                },
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "Основное меню", callbackData: "/start")
                }
            });
            ;
        }
        public static IReplyMarkup Form()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithUrl(text:"Анкета", url: Links.form),
                InlineKeyboardButton.WithCallbackData(text: "Основное меню", callbackData: "/start")
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
        public static IReplyMarkup WebCourses()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "Веб-Дизайн", callbackData: "Веб-Дизайн")
                },
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "Java- и  TypeScript", callbackData: "Java- и  TypeScript")
                },
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "JavaScript и фреймворки", callbackData: "JavaScript и фреймворки")
                },
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "Курсы", callbackData: "Курсы"),
                    InlineKeyboardButton.WithCallbackData(text: "Основное меню", callbackData: "/start")
                }
            });
            ;
        }
        public static IReplyMarkup ProgrammingCourses()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                new List<InlineKeyboardButton>
                { 
                    InlineKeyboardButton.WithCallbackData(text: "Основы программирования", callbackData: "Основы программирования")
                },
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "ООП", callbackData: "ООП")
                },
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "Курсы", callbackData: "Курсы"),
                    InlineKeyboardButton.WithCallbackData(text: "Основное меню", callbackData: "/start") 
                }
            });
            ;
        }
        public static IReplyMarkup HededCourses()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                new List<InlineKeyboardButton>
                { 
                    InlineKeyboardButton.WithCallbackData(text: "1️⃣ Веб направление", callbackData: "Веб направление") 
                },
                new List<InlineKeyboardButton>
                { 
                    InlineKeyboardButton.WithCallbackData(text:"2️⃣ Высокоуровневые языки и ООП", callbackData: "Языки и ООП") 
                },
                new List<InlineKeyboardButton>
                { 
                    InlineKeyboardButton.WithCallbackData(text:"3️⃣ Репетиторство", callbackData: "Репетиторство") 
                },
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "Основное меню", callbackData: "/start") 
                }
            });
            ;
        }
        public static IReplyMarkup DocumentLink(string link)
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>{ InlineKeyboardButton.WithUrl(text: "Материалы", url: link) }
            });
            ;
        }
        public static IReplyMarkup ToUsers()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "Пользователи", callbackData: "users")
                }
            });
            ;
        }
        public static IReplyMarkup UsersMenu(string id)
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "id"+id, callbackData: "change"+id)
                }
            });
            ;
        }
        public static IReplyMarkup InChange(string id)
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text:"Имя", callbackData: "name"+id)
                },
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text:"Курс", callbackData: "cour"+id),
                    InlineKeyboardButton.WithCallbackData(text:"Материалы", callbackData: "link"+id)
                },
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text:"Описание", callbackData: "desc"+id),
                    InlineKeyboardButton.WithCallbackData(text:"Заметка", callbackData: "note"+id)
                },
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "Удалить пользователя", callbackData: "del"+id)
                },
                new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(text: "Свернуть", callbackData: "user"+id)
                }
            });
            ;
        }
    }
}
