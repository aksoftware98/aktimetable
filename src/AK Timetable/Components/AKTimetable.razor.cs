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
        public EventCallback<Timetable> TimetableChanged { get; set; }

        [Parameter]
        public DateTime Day { get; set; }

        [Parameter]
        public List<TimetableBlock> SelectedBlocks { get; set; } = new List<TimetableBlock>();

        [Parameter]
        public EventCallback<List<TimetableBlock>> SelectedBlocksChanged { get; set; }

        [Parameter]
        public int ExportingNumber { get; set; }

        [CascadingParameter]
        public bool IsBusy { get; set; }

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
            if (!e.CtrlKey && !e.ShiftKey)
                SelectedBlocks.Clear();

            if (e.ShiftKey)
            {
                var lastBlock = SelectedBlocks.LastOrDefault();
                int difference = block.Number - lastBlock.Number;
                var emptyBlocks = Timetable.Blocks.Where(t => t.Number > lastBlock.Number && t.Number <= block.Number);
                if (lastBlock != null)
                {
                    foreach (var item in emptyBlocks)
                    {
                        SelectUnselectedBlock(item);
                    }
                }
            }
            else
            {
                SelectUnselectedBlock(block);
            }
            
           

            SelectedBlocksChanged.InvokeAsync(SelectedBlocks);
            TimetableChanged.InvokeAsync(Timetable);
        }

        private void SelectUnselectedBlock(TimetableBlock block)
        {
            var timetableBlock = Timetable.Blocks.SingleOrDefault(b => b.StartDate == block.StartDate);
            if (CheckSelection(block))
            {
                SelectedBlocks.Remove(block);
                timetableBlock.WorkItem = null;
            }
            else
            {
                SelectedBlocks.Add(block);
            }
        }

        internal void SetWorkItem(WorkItem workItem)
        {
            foreach (var item in SelectedBlocks)
            {
                item.WorkItem = workItem;
            }
            TimetableChanged.InvokeAsync(Timetable);
            StateHasChanged();
        }

        //private void MouseOver(MouseEventArgs args, TimetableBlock block)
        //{
        //    if (args.Buttons == 1)
        //        SelectBlock(block);
        //}

    }
}
