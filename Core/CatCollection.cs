// CatProtApp (c) 2025 Baltasar MIT License <jbgarcia@uvigo.es>


namespace CatProtApp.Core;


using System;
using System.Collections.Generic;


public class CatCollection {
    public CatCollection()
    {
        this._cats = [];
        this._cursor = 0;
    }

    public void Add(Cat cat)
    {
        this._cats.Add( cat );
    }

    public void Remove(Cat cat)
    {
        this._cats.Remove( cat );
    }

    public Cat GetAtCursor()
    {
        if ( this._cursor < 0
          || this._cursor >= this._cats.Count )
        {
            throw new IndexOutOfRangeException(
                        $"cursor out of range: {this._cursor} / {this._cats.Count}" );
        }

        return this._cats[ this._cursor ];
    }

    public Cat this[int index]
    {
        get {
            if ( index < 0
              || index >= this._cats.Count )
            {
                throw new IndexOutOfRangeException(
                            $"index out of range: {index} / {this._cats.Count}" );
            }

            return this._cats[ index ];
        }
    }

    public void Next()
        => this._cursor = Math.Max( 0, Math.Min( this._cats.Count - 1, this._cursor + 1 ));

    public void Prev() => this._cursor = Math.Max( 0, this._cursor - 1 );

    public void ResetCursor() => this._cursor = 0;

    public bool IsCursorValid()
            => this._cursor >= 0
            && this._cursor < this._cats.Count;

    public int Cursor => this._cursor;

    public int Count => this._cats.Count;

    public IReadOnlyList<Cat> GetAll() => this._cats.AsReadOnly();

    public override string ToString() => string.Join( ", ", this._cats );

    private readonly IList<Cat> _cats;
    private int _cursor;
}
