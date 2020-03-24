using Syncfusion.SfChart.XForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;

namespace SimpleSample
{
    public class PieChartExt : SfChart
    {

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(object), typeof(PieChartExt), null, BindingMode.Default, null, OnItemsSourceChanged);

        public static readonly BindableProperty XBindingPathProperty =
            BindableProperty.Create("XBindingPath", typeof(string), typeof(PieChartExt), "XValue", BindingMode.Default, null);

        public static readonly BindableProperty YBindingPathProperty =
                BindableProperty.Create(nameof(YBindingPath), typeof(string), typeof(PieChartExt), "YValue", BindingMode.Default, null);

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public string XBindingPath
        {
            get { return (string)GetValue(XBindingPathProperty); }
            set { SetValue(XBindingPathProperty, value); }
        }

        public string YBindingPath
        {
            get { return (string)GetValue(YBindingPathProperty); }
            set { SetValue(YBindingPathProperty, value); }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as PieChartExt).GenerateSeries(newValue);
        }

        private void GenerateSeries(object newValue)
        {
            if (ItemsSource != null)
            {
                var commonItemsSource = (ItemsSource as IEnumerable).GetEnumerator();

                if (newValue is INotifyCollectionChanged)
                    (newValue as INotifyCollectionChanged).CollectionChanged += DataPoint_CollectionChanged;

                while (commonItemsSource.MoveNext())
                {
                    CreateSeries(commonItemsSource.Current);
                }
            }
        }

        //Add and removed the series dynamically
        private void DataPoint_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    CreateSeries(e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Series.RemoveAt(e.OldStartingIndex);
                    break;
            }
        }

        private void CreateSeries(object newValue)
        {
            StackingBar100Series stackingBar100Series = new StackingBar100Series()
            {
                ItemsSource = new List<PriceData> { newValue as PriceData },
                XBindingPath = XBindingPath,
                YBindingPath = YBindingPath,
                Width = 0.7,
            };
            
            stackingBar100Series.DataMarker = new ChartDataMarker();
            stackingBar100Series.DataMarker.ShowLabel = true;
            stackingBar100Series.DataMarker.LabelStyle.TextColor = Color.White;
            stackingBar100Series.DataMarkerPosition = DataMarkerPosition.Center;
            Series.Add(stackingBar100Series);
        }
    }
}
