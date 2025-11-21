// CatProtApp (c) 2025 Baltasar MIT License <jbgarcia@uvigo.es>


namespace CatProtApp.View;


using System;
using System.Diagnostics;
using System.Collections.Generic;
using Avalonia.Controls;


public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();

        // Lookup controls
        this._edName = this.GetControl<TextBox>( "EdName" );
        this._edRace = this.GetControl<ComboBox>( "EdRace" );
        this._btNext = this.GetControl<Button>( "BtNext" );
        this._btPrev = this.GetControl<Button>( "BtPrev" );
        this._btNew = this.GetControl<Button>( "BtNew" );
        this._edBirthDate = this.GetControl<DatePicker>( "EdBirthDate" );
        this._lblStatus = this.GetControl<Label>( "LblStatus" );

        this.OnNext = this.OnPrev = this.OnNew = () => {};
        this._btNext.Click += (_, _) => this.OnNext();
        this._btPrev.Click += (_, _) => this.OnPrev();
        this._btNew.Click += (_, _) => this.OnNew();
    }

    internal void Init(IReadOnlyList<string> raceLabels)
    {
        // Set the items of the combobox
        this._edRace.ItemsSource = raceLabels;
        this._edRace.SelectedIndex = 0;
    }

    internal void UpdateStatus(int pos, int numCats)
    {
        // Status
        this._lblStatus.Content = "0 / 0";

        // Current cat
        if ( pos >= 0 ) {
            this._lblStatus.Content = $"{pos + 1} / {numCats}";
        }
    }

    public int GetCurrentRaceIndex() => this._edRace.SelectedIndex;

    public DateTimeOffset GetCurrentBirth()
        => this._edBirthDate.SelectedDate.GetValueOrDefault(
                                            new DateTimeOffset( DateTime.Today ));
    public string GetCurrentName() => ( this._edName.Text ?? "" ).Trim();

    public void SetCurrentName(string name) => this._edName.Text = name;
    public void SetCurrentRaceIndex(int index)
    {
        Debug.Assert( index >= 0 && index < this._edRace.Items.Count );
        this._edRace.SelectedIndex = index;
    }
    public void SetCurrentBirth(DateTime birth)
        => this._edBirthDate.SelectedDate = new DateTimeOffset( birth );

    public Action OnNext;
    public Action OnPrev;
    public Action OnNew;
    private readonly ComboBox _edRace;
    private readonly Button _btNew;
    private readonly Button _btNext;
    private readonly Button _btPrev;
    private readonly DatePicker _edBirthDate;
    private readonly Label _lblStatus;
    private readonly TextBox _edName;
}
