using RestSharp;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;

namespace KinoBot{
    
    class Programm
    {
        private static ReplyKeyboardMarkup? _replyMarkup;
        private static Dictionary<long, bool[]> _isProgress;
        private static readonly string?[] Tokens = new string?[2];

        private static void AddHead(RestRequest? request)
        {
            if (request != null)
            {
                request.AddHeader("accept", "application/json");
                request.AddHeader("X-API-KEY", Tokens[0]);
            }
        }

        static async Task GetTokens()
        {
            StreamReader sr = new StreamReader( "../../../tokens.txt");
            Tokens[0] = await sr.ReadLineAsync();
            Tokens[1] = await sr.ReadLineAsync();
            sr.Close();
        }
        
        static async Task Main()
        {
            await GetTokens();
            if (Tokens[1] != null)
            {
                var client = new TelegramBotClient(Tokens[1]);
                _isProgress = new Dictionary<long, bool[]>();
                _replyMarkup = new ReplyKeyboardMarkup((KeyboardButton[])[
                    new KeyboardButton("Найти фильм по названию"),
                    new KeyboardButton("Получить радномный тайтл"),
                    new KeyboardButton("Найти актёра по имени")
                ])
                {
                    ResizeKeyboard = true,
                    OneTimeKeyboard = true
                };
                client.StartReceiving(Update, Error);
            }

            Console.ReadLine();
        }

        static async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            if(message != null && !_isProgress.ContainsKey(message.Chat.Id)) _isProgress.Add(message.Chat.Id, [false,false]);
            if (message?.Text != null)
            {
               Console.WriteLine($"{message.Chat.Username} : {message.Text} ");
               var bools = _isProgress[message.Chat.Id];
               
                if (message.Text.ToLower().Contains("/start"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Выбери пункт меню", replyMarkup: _replyMarkup, cancellationToken: token);
                }
                
                if (bools[0] == false && bools[1] == false && message.Text == "Найти фильм по названию")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Напиши название фильма", cancellationToken: token);
                    if (_replyMarkup != null) _replyMarkup.IsPersistent = false;
                    bools[0] = true;
                }
                else if (bools[0])
                {
                    var client = new RestClient(new RestClientOptions("https://api.kinopoisk.dev/v1.4/movie/search?page=1&limit=1&query=" + message.Text));
                    var request = new RestRequest("");
                    AddHead(request);
                    
                    var response = await client.GetAsync(request, cancellationToken: token);
                    
                    Console.WriteLine(response.Content);
                    if (response.Content != null)
                    {
                        var root = JsonConvert.DeserializeObject<RootMovie>(response.Content);
                    
                        if (root?.total != 0)
                        {
                            var url = root?.docs[0].poster?.previewUrl;
                            var text = "";
                            if (root?.docs[0].name != "null") text += "Название: " + root?.docs[0].name;
                            if (root?.docs[0].rating?.kp != null) text += "\n\nРейтинг КиноПоиска: " + root?.docs[0].rating?.kp;
                            if (root?.docs[0].description != "null") text += "\n\nОписание: " + root?.docs[0].description;
                        
                            if (!string.IsNullOrEmpty(url))
                                await botClient.SendPhotoAsync(message.Chat.Id, new InputFileUrl(url), caption: text, cancellationToken: token);
                            else 
                                await botClient.SendTextMessageAsync(message.Chat.Id, text, cancellationToken: token);

                        }
                        else
                            await botClient.SendTextMessageAsync(message.Chat.Id,
                                "По вашему запросу ничего не найдено. Попробуйте ещё раз", cancellationToken: token);
                    }

                    bools[0] = false;
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите пункт меню", replyMarkup: _replyMarkup, cancellationToken: token);
                }
                else if (bools[0] == false && bools[1] == false && message.Text == "Найти актёра по имени")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Напиши имя актёра", cancellationToken: token);
                    
                    bools[1] = true;
                }
                else if (bools[1])
                {
                    var client = new RestClient(new RestClientOptions("https://api.kinopoisk.dev/v1.4/person/search?page=1&limit=1&query=" + message.Text));
                    var request = new RestRequest("");
                    AddHead(request);
                    
                    var response = await client.GetAsync(request, cancellationToken: token);
                    
                    Console.WriteLine(response.Content);
                    if (response.Content != null)
                    {
                        var root = JsonConvert.DeserializeObject<RootPerson>(response.Content);
                    
                        if (root?.total != 0)
                        {
                            var url = root?.docs[0].photo;
                            var text = "";
                            if (root?.docs[0].name != "") text += "Имя: " + root?.docs[0].name;
                            if (root?.docs[0].sex != "") text += "\n\nПол: " + root?.docs[0].sex;
                            if (root?.docs[0].age != 0) text += "\n\nВозраст: " + root?.docs[0].age;
                            if (root?.docs[0].growth != 0) text += "\n\nРост: " + root?.docs[0].growth;
                        
                            if (!string.IsNullOrEmpty(url))
                                await botClient.SendPhotoAsync(message.Chat.Id, new InputFileUrl(url), caption: text, cancellationToken: token);
                            else 
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Без фотки отправлять не очень, но такой человек есть.", cancellationToken: token);
                        }
                        else
                            await botClient.SendTextMessageAsync(message.Chat.Id,
                                "По вашему запросу ничего не найдено. Попробуйте ещё раз", cancellationToken: token);
                    }

                    bools[1] = false;
                    
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите пункт меню", replyMarkup: _replyMarkup, cancellationToken: token);
                }
                else if (bools[0] == false && bools[1] == false && message.Text == "Получить радномный тайтл")
                {
                    var client = new RestClient(new RestClientOptions("https://api.kinopoisk.dev/v1.4/movie/random?lists=top250"));
                    var request = new RestRequest("");
                    AddHead(request);
                    
                    var response = await client.GetAsync(request, cancellationToken: token);
                    
                    Console.WriteLine(response.Content);
                    if (response.Content != null)
                    {
                        var root = JsonConvert.DeserializeObject<RootRandMovie>(response.Content);
                    
                        var url = root?.poster.previewUrl;
                        var text = "";
                        if (root?.name != "null") text += "Название: " + root?.name;
                        if (root?.rating.kp != null) text += "\n\nРейтинг КиноПоиска: " + root?.rating.kp;
                        if (root?.description != "null") text += "\n\nОписание: " + root?.description;
                        
                        if (!string.IsNullOrEmpty(url))
                            await botClient.SendPhotoAsync(message.Chat.Id, new InputFileUrl(url), caption: text, cancellationToken: token);
                        else 
                            await botClient.SendTextMessageAsync(message.Chat.Id, text, cancellationToken: token);
                    }

                    await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите пункт меню", replyMarkup: _replyMarkup, cancellationToken: token);
                }
            }
        }
        
        private static Task Error(ITelegramBotClient botClient, Exception e, CancellationToken arg3)
        {
            Console.WriteLine(e.Message);
            return Task.CompletedTask;
        }

        
    }
}
