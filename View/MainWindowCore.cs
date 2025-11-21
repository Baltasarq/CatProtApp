// CatProtApp (c) 2025 Baltasar MIT License <jbgarcia@uvigo.es>


namespace CatProtApp.View;


using System;
using System.Collections.Generic;

using Core;


public class MainWindowCore {
    public MainWindowCore(MainWindow? view = null)
    {
        this.View = view ?? new MainWindow();

        // Init widgets
        this.View.Init( Cat.RaceLabels );
        this.View.OnNext = () => this.Next();
        this.View.OnPrev = () => this.Prev();
        this.View.OnNew = () => this.New();

        // Goto 0
        this._cats = new CatCollection();
        this.Update();
    }

    /// <summary>Updates the view.</summary>
    private void Update()
    {
        int pos = -1;

        if ( this._cats.IsCursorValid() ) {
            var cat = this._cats.GetAtCursor();

            pos = this._cats.Cursor;
            this.View.SetCurrentName( cat.Name );
            this.View.SetCurrentRaceIndex( (int) cat.Race );
            this.View.SetCurrentBirth( cat.Birth );
        }

        this.View.UpdateStatus( pos, this._cats.Count );
    }

    /// <summary>Change to the next element, if it exists.</summary>
    private void Next()
    {
        this._cats.Next();
        this.Update();
    }

    /// <summary>Change to the previous element, if it exists.</summary>
    private void Prev()
    {
        this._cats.Prev();
        this.Update();
    }

    /// <summary>Creates a new cat, and adds it to the list.</summary>
    private void New()
    {
        var race = Enum.GetValues<Cat.Races>()[ this.View.GetCurrentRaceIndex() ];
        var cat = new Cat{  Name = this.View.GetCurrentName(),
                            Race = race,
                            Birth = this.View.GetCurrentBirth().Date };
        this._cats.Add( cat );
        this.Update();
    }

    public MainWindow View { get; private set; }
    private readonly CatCollection _cats;
}