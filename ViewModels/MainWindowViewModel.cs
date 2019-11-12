using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BarcodeNETCore.ViewModels
{
	public class MainWindowViewModel : BindableBase, INotifyPropertyChanged
	{
		public ReactivePropertySlim<string> Title { get; } = new ReactivePropertySlim<string>("Barcode Application");

		private CompositeDisposable Disposable { get; } = new CompositeDisposable();

		public ReactiveCommand<KeyEventArgs> BarcodeEvent { get; } = new ReactiveCommand<KeyEventArgs>();

		public ReactivePropertySlim<string> KeyData { get; } = new ReactivePropertySlim<string>();

		public string strBuffer = string.Empty;


		public MainWindowViewModel()
		{
			BarcodeEvent.Subscribe(BarcodeInput).AddTo(Disposable);
		}

		~MainWindowViewModel()
		{
			Disposable.Dispose();
		}

		/// <summary>
		/// バーコード入力
		/// </summary>
		/// <param name="e"></param>
		private void BarcodeInput(KeyEventArgs e)
		{
			if (e.Key != Key.LeftShift && e.Key != Key.Enter)
			{
				string str = e.Key.ToString();
				strBuffer += str.Substring(str.Length - 1, 1);
			}
			// 文字列が改行コードなら確定なので文字列処理を開始する
			if (e.Key == Key.Enter)
			{
				KeyData.Value = strBuffer;
				strBuffer = string.Empty;
			}
		}
	}
}
