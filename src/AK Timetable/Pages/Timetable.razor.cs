using AK_Timetable.Components;
using AK_Timetable.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AK_Timetable.Pages
{
    [Authorize]
    public partial class Timetable : ComponentBase
    {

        [Inject]
        public GraphServiceClient Graph { get; set; }

        [Inject]
        public MicrosoftIdentityConsentAndConditionalAccessHandler ConsentHandler { get; set; }

        private AKTimetable _timetable;
        private bool _isBusy = false;
        private void OnSelectWorkItem(WorkItem workItem)
        {
            _timetable.SetWorkItem(workItem);
        }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                await Graph.Me.Request().GetAsync();
            }
            catch (Exception ex)
            {
                ConsentHandler.HandleException(ex);
            }
        }

        private int _indexExport = 0;

        private async Task ExportToMicrosoftAsync()
        {
            _isBusy = true; 
            _indexExport = 0;
            StateHasChanged(); 
            try
            {
                NumberBlocks();
                await RemoveAllEvents();
                var groupedItems = (from i in _timetable.Timetable.Blocks
                                    where i.WorkItem != null
                                    group i by i.Sequence into g
                                    select g).ToArray();

                foreach (var items in groupedItems)
                {
                    var workItem = items.FirstOrDefault();
                    var lastWorkItem = items.LastOrDefault();

                    if (await CheckIfExportedAsync(workItem.WorkItem, workItem.StartDate, lastWorkItem.EndDate))
                        continue;

                    await AddEventAsync(workItem.WorkItem.Name, workItem.StartDate, lastWorkItem.EndDate);
                    _indexExport++;
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                ConsentHandler.HandleException(ex);
            }
            _isBusy = false;
            StateHasChanged();
        }

        private async Task<bool> CheckIfExportedAsync(WorkItem workItem, DateTime startDate, DateTime endDate)
        {
            string filter = $"start/datetime ge '{startDate.ToUniversalTime():yyyy-MM-ddTHH:mm}' and end/datetime lt '{endDate.ToUniversalTime():yyyy-MM-ddTHH:mm}' and subject eq '{workItem.Name}'";
            var blockEvent = await Graph.Me.Calendar.Events.Request().Filter(filter).GetAsync();
            
            return blockEvent.Any();
        }

        private async Task RemoveAllEvents()
        {
            string filter = $"start/datetime ge '{DateTime.Now.Date.ToUniversalTime():yyyy-MM-ddTHH:mm}' and end/datetime lt '{DateTime.Now.AddDays(1).Date.ToUniversalTime():yyyy-MM-ddTHH:mm}'";
            var blockEvent = await Graph.Me.Calendar.Events.Request().Filter(filter).GetAsync();
            if (!blockEvent.Any())
                return; 

            foreach (var item in blockEvent.Where(e => e.Categories.Any(e => e == "ak_event")))
            {
                var request = Graph.Me.Events.Request().GetHttpRequestMessage();
                request.RequestUri = new Uri($"https://graph.microsoft.com/v1.0/me/events/{item.Id}");
                request.Method = HttpMethod.Delete;
                await Graph.HttpProvider.SendAsync(request);
            }
        }

        private async Task AddEventAsync(string name, DateTime start, DateTime end)
        {
            await Graph.Me.Calendar.Events.Request().AddAsync(new Event
            {
                Subject = name,
                Start = DateTimeTimeZone.FromDateTime(start),
                End = DateTimeTimeZone.FromDateTime(end),
                Categories = new[] { "ak_event" }
            });
        }


        private void NumberBlocks()
        {
            var i = 0;
            WorkItem previousItem = null;
            foreach (var item in _timetable.Timetable.Blocks)
            {
                if (item.WorkItem != previousItem)
                {
                    item.Sequence = ++i;
                }
                else
                    item.Sequence = i;
                previousItem = item.WorkItem;
                Debug.WriteLine(item.Sequence);
            }
        }

    }
}
