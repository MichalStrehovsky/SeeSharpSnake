namespace System
{
    public static class Environment
    {
        public static unsafe long TickCount64
        {
            get
            {
                EfiRuntimeHost.SystemTable->RuntimeServices->GetTime(out var time, out var capabilities);
                var daysCount = time.Year * 365 + time.Month * 31 + time.Day;
                long secondsCount = daysCount * 24 * 60 * 60 + time.Hour * 60 * 60 + time.Minute * 60 + time.Second;
                long timerTicks = secondsCount * 1000 + time.Nanosecond / 1000_000;

                // Deliberately change time scale.
                // This increase game loop poll iteration, and make game smooth.
                return timerTicks / 1;
            }
        }
    }
}
