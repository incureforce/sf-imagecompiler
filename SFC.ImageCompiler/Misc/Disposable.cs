using System;

namespace SFC.ImageCompiler
{
    public class Disposable : IDisposable
    {
        public event EventHandler Callback;

        public void Dispose()
        {
            var callback = Callback;
            if (callback != null) {
                Callback = null;

                callback(this, EventArgs.Empty);
            }
        }
    }
}
