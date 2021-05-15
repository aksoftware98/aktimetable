using AK_Timetable.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Timetable.Components
{
    public partial class AKTimetable : ComponentBase
    {

        [Parameter]
        public Timetable Timetable { get; set; }

        [Parameter]
        public DateTime Day { get; set; }

        [Parameter]
        public TimetableBlock SelectedBlock { get; set; }

        [Parameter]
        public EventCallback<TimetableBlock> SelectedBlockChanged { get; set; }

        protected override void OnParametersSet()
        {
            if (Timetable == null)
                Timetable = new Timetable();
        }

        private void SelectBlock(TimetableBlock block)
        {
            if (SelectedBlock == block)
            {
                SelectedBlock = null;
            }
            else
            {
                SelectedBlock = block;
            }
            SelectedBlockChanged.InvokeAsync(block);
        }

        internal void SetWorkItem(WorkItem workItem)
        {
            if (SelectedBlock != null)
                SelectedBlock.WorkItem = workItem;
            StateHasChanged();
        }

        private void MouseOver(MouseEventArgs args, TimetableBlock block)
        {
            if (args.Button == 0)
                SelectBlock(block);
        }

    }
}
