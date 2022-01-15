namespace SeeSharpSnake.Game
{
    unsafe struct Music
    {
        internal const int notesLength = 26;
        const int quarterNote = 16;

        internal fixed uint NoteFrequencies[notesLength];
        internal fixed uint NoteDurations[notesLength];

        static int tuneLength = 60;
        static int noteFrame = 0;
        static int tuneFrame = 0;
        static int tunePos = 0;

        void AddNote(uint f, uint d, int ix)
        {
            NoteFrequencies[ix] = f;
            NoteDurations[ix] = d;
        }

        // Thankyou Matthew Smith!
        public void SetUp()
        {
            int i = 0;
            // bar 1, 7 notes
            AddNote(123, quarterNote, i++); // b3
            AddNote(138, quarterNote, i++); //  c#4
            AddNote(146, quarterNote, i++); //  d4
            AddNote(164, quarterNote, i++); //  e4
            AddNote(184, quarterNote, i++); //  F#4
            AddNote(146, quarterNote, i++); //  d4
            AddNote(184, quarterNote * 2, i++); //  F#4

            // bar 2, 6 notes, 13 total
            AddNote(174, quarterNote, i++); //  F4
            AddNote(138, quarterNote, i++); //  c#4
            AddNote(174, quarterNote * 2, i++); //  F4
            AddNote(164, quarterNote, i++); //  e4
            AddNote(130, quarterNote, i++); //  c4
            AddNote(164, quarterNote * 2, i++); //  e4

            // bar 3, 8 notes, 21 total
            AddNote(123, quarterNote, i++); // b3
            AddNote(138, quarterNote, i++); //  c#4
            AddNote(146, quarterNote, i++); //  d4
            AddNote(164, quarterNote, i++); //  e4
            AddNote(184, quarterNote, i++); //  F#4
            AddNote(146, quarterNote, i++); //  d4
            AddNote(184, quarterNote, i++); //  F#4
            AddNote(246, quarterNote, i++); // b4

            // bar 4, 5 notes, 26 total, ok so this is the original not the MS version, shoot me.
            AddNote(219, quarterNote, i++); // a4
            AddNote(184, quarterNote, i++); //  F#4
            AddNote(219, quarterNote, i++); // a4
            AddNote(184, quarterNote, i++); //  F#4
            AddNote(219, quarterNote * 4, i++); // a4
        }

        internal void PlayTune()
        {
            // next note?
            if (noteFrame == NoteDurations[tunePos])
            {
                tunePos++;
                noteFrame = 0;

                // loop?
                if (tunePos == notesLength)
                {
                    tunePos = 0;
                }
            }

            if (noteFrame == 0)
            {
                W4.tone(NoteFrequencies[tunePos], NoteDurations[tunePos], 70, 0);
            }

            noteFrame++;
            tuneFrame++;
            // loop
            if (tuneFrame == tuneLength)
            {
                tuneFrame = 0;
            }
        }

    }

}
