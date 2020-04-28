using System;
using NAudio.Wave;

namespace AuroraLoader
{
    class Song : IDisposable
    {
        public readonly string File;
        public bool Playing { get { return WaveOut?.PlaybackState == PlaybackState.Playing; } }
        public double Volume
        {
            get
            {
                if (WaveOut != null)
                {
                    return WaveOut.Volume;
                }
                else
                {
                    return 0.5;
                }
            }
            set
            {
                if (WaveOut != null)
                {
                    WaveOut.Volume = (float)value;
                }
            }
        }

        private WaveOutEvent WaveOut { get; set; } = null;
        private AudioFileReader Reader { get; set; } = null;
        private bool Initialized { get; set; } = false;

        public Song(string file)
        {
            if (!System.IO.File.Exists(file))
            {
                throw new Exception("File not found: " + file);
            }

            File = file;
        }

        public void Play()
        {
            if (!Initialized)
            {
                Initialize();
            }

            if (Playing)
            {
                return;
            }

            Reader.Position = 0;
            WaveOut.Play();
        }

        public void Stop()
        {
            if (!Initialized)
            {
                return;
            }

            if (!Playing)
            {
                return;
            }

            WaveOut.Stop();
        }

        private void Initialize()
        {
            WaveOut = new WaveOutEvent();
            Reader = new AudioFileReader(File);
            WaveOut.Init(Reader);

            Initialized = true;
        }

        #region IDisposable Support
        private bool DisposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!DisposedValue)
            {
                if (disposing)
                {
                    WaveOut?.Dispose();
                    Reader?.Dispose();

                    WaveOut = null;
                    Reader = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                DisposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Song()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
