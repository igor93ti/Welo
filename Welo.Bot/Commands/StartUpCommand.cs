using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Welo.Application.AppServices;
using Welo.Domain.Entities;

namespace Welo.Bot.Commands
{
    [Serializable]
    public class StartUpCommand : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var trigger = message.Text;
            var response = StandardCommandsAppService.Intance.GeneralCommand(trigger);
            var activity = context.MakeMessage();
            if (response == null)
            {
                context.Done(string.Empty);
                return;
            }

            if (response.WithButtons)
            {
                PromptDialog.Choice(
                  context,
                  this.OnOptionSelected,
                  response.Buttons.Select(x => x.Value),
                  response.MessageFormated,
                  null,
                  3,
                  PromptStyle.Keyboard,
                  response.Buttons.Select(x => x.Title));
            }
            else
            {
                activity.Text = response.MessageFormated;
                context.Done(activity);
            }
            //var card = CreateCardMessage(response);
            //activity.Attachments.Add(card?.ToAttachment());

            //var flightAttachment = GetFlightAttachment();


            //if (message.ChannelId != "facebook")
            //{
            //    activity.Text = flightAttachment.ToString();
            //}
            //else
            //{
            //    activity.ChannelData = new FacebookChannelData()
            //    {
            //        Attachment = flightAttachment
            //    };
            //}

            //context.Done(activity);

        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                var optionSelected = await result;
                var activity = context.MakeMessage();
                activity.Text = optionSelected;
                var root = new RootDialog();
                await context.Forward(root, root.ResumeAfterDialog, activity, CancellationToken.None);

            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");
                context.Wait(this.MessageReceivedAsync);
            }
        }

        private static HeroCard CreateCardMessage(Option response)
        {
            if (response == null)
                return null;

            var cardButtons = new List<CardAction>();
            var lista = response.Buttons;

            foreach (var item in lista)
            {
                cardButtons.Add(new CardAction
                {
                    Value = item.Value,
                    Type = Mapper.Map<TypeButton, string>(item.Type),
                    Title = item.Title
                });
            }
            var openUrl = new CardAction()
            {
                Value = response.Link,
                Type = "openUrl"
            };

            return new HeroCard
            {
                //Title = response.Title,
                //Subtitle = response.Author,
                Text = response.MessageFormated,
                Images = new List<CardImage> {
                   new CardImage
                    {
                        Tap = openUrl,
                        Url = response.Image
                    }
                },
                Tap = openUrl,
                Buttons = cardButtons
            };

        }
        private static FacebookAttachment GetFlightAttachment()
        {
            return new FacebookAttachment()
            {
                Payload = new AirlineCheckIn()
                {
                    IntroMessage = "Check-in is available now",
                    Locale = "en_US",
                    PnrNumber = "ABCDEF",
                    CheckInUrl = "http://www.airline.com/check_in",
                    FlightInfo = new[]
                    {
                        new FlightInfo()
                        {
                            FlightNumber = "F001",
                            DepartureAirport = new Airport()
                            {
                                AirportCode = "SFO",
                                City = "San Francisco",
                                Terminal = "T4",
                                Gate = "G8"
                            },
                            ArrivalAirport = new Airport()
                            {
                                AirportCode = "EZE",
                                City = "Buenos Aires",
                                Terminal = "C",
                                Gate = "A2"
                            },
                            FlightSchedule = new FlightSchedule()
                            {
                                BoardingTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-ddTH:mm"),
                                DepartureTime = DateTime.Now.AddDays(1).AddHours(1.5).ToString("yyy-MM-ddTH:mm"),
                                ArrivalTime = DateTime.Now.AddDays(2).ToString("yyyy-MM-ddTH:mm")
                            }
                        }
                    }
                }
            };
        }
    }

    public class FacebookChannelData
    {
        [JsonProperty("attachment")]
        public FacebookAttachment Attachment { get; internal set; }
    }
    public class FacebookAttachment
    {
        public FacebookAttachment()
        {
            this.Type = "template";
        }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("payload")]
        public dynamic Payload { get; set; }

        public override string ToString()
        {
            return this.Payload.ToString();
        }
    }

    public class Airport
    {
        [JsonProperty("airport_code")]
        public string AirportCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("terminal")]
        public string Terminal { get; set; }

        [JsonProperty("gate")]
        public string Gate { get; set; }
    }
    public class AirlineCheckIn
    {
        public AirlineCheckIn()
        {
            this.TemplateType = "airline_checkin";
        }

        [JsonProperty("template_type")]
        public string TemplateType { get; set; }

        [JsonProperty("intro_message")]
        public string IntroMessage { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("pnr_number")]
        public string PnrNumber { get; set; }

        [JsonProperty("flight_info")]
        public FlightInfo[] FlightInfo { get; set; }

        [JsonProperty("checkin_url")]
        public string CheckInUrl { get; set; }

        public override string ToString()
        {
            return $"{this.IntroMessage}. Confirmation Number: {this.PnrNumber}. Flight {this.FlightInfo[0].FlightNumber} from {this.FlightInfo[0].DepartureAirport.City} ({this.FlightInfo[0].DepartureAirport.AirportCode}) to {this.FlightInfo[0].ArrivalAirport.City} to ({this.FlightInfo[0].ArrivalAirport.AirportCode}) departing at {this.FlightInfo[0].FlightSchedule.DepartureTime} from gate {this.FlightInfo[0].DepartureAirport.Gate} at terminal {this.FlightInfo[0].DepartureAirport.Terminal} and arriving at {this.FlightInfo[0].FlightSchedule.ArrivalTime} to gate {this.FlightInfo[0].ArrivalAirport.Gate} at terminal {this.FlightInfo[0].ArrivalAirport.Terminal}. Boarding time is {this.FlightInfo[0].FlightSchedule.BoardingTime}. Check in @ {this.CheckInUrl}";
        }
    }

    public class FlightInfo
    {
        [JsonProperty("flight_number")]
        public string FlightNumber { get; set; }

        [JsonProperty("departure_airport")]
        public Airport DepartureAirport { get; set; }

        [JsonProperty("arrival_airport")]
        public Airport ArrivalAirport { get; set; }

        [JsonProperty("flight_schedule")]
        public FlightSchedule FlightSchedule { get; set; }
    }
    public class FlightSchedule
    {
        [JsonProperty("boarding_time")]
        public string BoardingTime { get; set; }

        [JsonProperty("departure_time")]
        public string DepartureTime { get; set; }

        [JsonProperty("arrival_time")]
        public string ArrivalTime { get; set; }
    }
}