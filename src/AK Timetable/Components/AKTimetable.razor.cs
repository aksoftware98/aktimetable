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
        public List<TimetableBlock> SelectedBlocks { get; set; } = new List<TimetableBlock>();

        [Parameter]
        public EventCallback<List<TimetableBlock>> SelectedBlocksChanged { get; set; }

        protected override void OnParametersSet()
        {
            if (Timetable == null)
                Timetable = new Timetable();
        }

        private bool CheckSelection(TimetableBlock block)
        {
            return SelectedBlocks.Contains(block);
        }

        private void SelectBlock(MouseEventArgs e, TimetableBlock block)
        {
            if (!e.CtrlKey)
                SelectedBlocks.Clear();
            
            if (CheckSelection(block))
            {
                SelectedBlocks.Remove(block);
            }
            else
            {
                SelectedBlocks.Add(block);
            }
            SelectedBlocksChanged.InvokeAsync(SelectedBlocks);
        }

        internal void SetWorkItem(WorkItem workItem)
        {
            foreach (var item in SelectedBlocks)
            {
                item.WorkItem = workItem;
            }
            StateHasChanged();
        }

        //private void MouseOver(MouseEventArgs args, TimetableBlock block)
        //{
        //    if (args.Buttons == 1)
        //        SelectBlock(block);
        //}

    }
}
