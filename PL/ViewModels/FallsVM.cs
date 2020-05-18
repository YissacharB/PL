using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using PL.Commands;
using PL.Models;

namespace PL.ViewModels
{
    public class FallsVM : IFallsVM
    {
        public OpenImageCommand openImageCommand { get; set; }
        public ObservableCollection<Fall> Falls { get; set; }
        public ManageFallsModel CurrentModel { get; set; }
        public PredictionAndRealFallsVM PredictionAndRealFalls { get; set; }
        public AddFallCommand Add { get; set; }

        public FallsVM()
        {
            PredictionAndRealFalls = PredictionAndRealFallsVM.Instance;
            openImageCommand = new OpenImageCommand();
            CurrentModel = new ManageFallsModel();
            Add = new AddFallCommand(this);
            Falls = new ObservableCollection<Fall>(CurrentModel.AllFalls());
            Falls.CollectionChanged += Falls_CollectionChanged;
        }

        private void Falls_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //add
                CurrentModel.AddFall(e.NewItems[0] as Fall);
                PredictionAndRealFalls.AddFall((e.NewItems[0] as Fall).location);
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                //remove
                var oldId = (e.OldItems[0] as Fall).id;
                CurrentModel.Remove(oldId);

            }

        }
        public Fall GetFall(int id)
        {
            return CurrentModel.Search(id);
        }
        public void AddFall(Fall fall)
        {
            Falls.Add(fall);
              CurrentModel.AddFall(fall);
        }

    }
}
