using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Pea.Networking.Messages
{

    public class BTimer : IDisposable
    {
        public static long CurrentTick { get; protected set; }
        public delegate void DoneHandler(bool isSuccessful);

        private static BTimer _instance;

        private List<Action> _mainThreadActions;

        /// <summary>
        /// Event, which is invoked every second
        /// </summary>
        public event Action<long> OnTick;

        public event Action ApplicationQuit;

        private readonly object _mainThreadLock = new object();

        private readonly System.Timers.Timer ticker;

        public static BTimer Instance
        {
            get
            {
                if (_instance == null)
                {

                    _instance = new BTimer();
                }
                return _instance;
            }
        }

        // Use this for initialization
        private BTimer()
        {


            _mainThreadActions = new List<Action>();
            _instance = this;

            CurrentTick = 0;
            ticker = new System.Timers.Timer();
            ticker.Elapsed += new ElapsedEventHandler(Tick);
            ticker.Interval = 1000;
            ticker.Start();


        }

        //void Update()
        //{
        //    if (_mainThreadActions.Count > 0)
        //    {
        //        lock (_mainThreadLock)
        //        {
        //            foreach (var actions in _mainThreadActions)
        //            {
        //                actions.Invoke();
        //            }

        //            _mainThreadActions.Clear();
        //        }
        //    }
        //}

        ///// <summary>
        /////     Waits while condition is false
        /////     If timed out, callback will be invoked with false
        ///// </summary>
        ///// <param name="condiction"></param>
        ///// <param name="doneCallback"></param>
        ///// <param name="timeoutSeconds"></param>
        //public static void WaitUntil(Func<bool> condiction, DoneHandler doneCallback, float timeoutSeconds)
        //{
        //    Instance.StartCoroutine(WaitWhileTrueCoroutine(condiction, doneCallback, timeoutSeconds, true));
        //}

        ///// <summary>
        /////     Waits while condition is true
        /////     If timed out, callback will be invoked with false
        ///// </summary>
        ///// <param name="condiction"></param>
        ///// <param name="doneCallback"></param>
        ///// <param name="timeoutSeconds"></param>
        //public static void WaitWhile(Func<bool> condiction, DoneHandler doneCallback, float timeoutSeconds)
        //{
        //    Instance.StartCoroutine(WaitWhileTrueCoroutine(condiction, doneCallback, timeoutSeconds));
        //}

        //private static IEnumerator WaitWhileTrueCoroutine(Func<bool> condition, DoneHandler callback,
        //    float timeoutSeconds, bool reverseCondition = false)
        //{
        //    while ((timeoutSeconds > 0) && (condition.Invoke() == !reverseCondition))
        //    {
        //        timeoutSeconds -= Time.deltaTime;
        //        yield return null;
        //    }

        //    callback.Invoke(timeoutSeconds > 0);
        //}

        //public static void AfterSeconds(float time, Action callback)
        //{
        //    Instance.StartCoroutine(Instance.StartWaitingSeconds(time, callback));
        //}

        //public static void ExecuteOnMainThread(Action action)
        //{
        //    Instance.OnMainThread(action);
        //}

        //public void OnMainThread(Action action)
        //{
        //    lock (_mainThreadLock)
        //    {
        //        _mainThreadActions.Add(action);
        //    }
        //}

        //private IEnumerator StartWaitingSeconds(float time, Action callback)
        //{
        //    yield return new WaitForSeconds(time);
        //    callback.Invoke();
        //}

        public void Tick(object sender, ElapsedEventArgs evt)
        {
            CurrentTick++;
            try
            {
                if (OnTick != null)
                    OnTick.Invoke(CurrentTick);
            }
            catch (Exception e)
            {
                //Logs.Error(e);
            }
        }

        public void Dispose()
        {
            if (ticker != null)
            {
                ticker.Stop();
                ticker.Dispose();
            }
            if (ApplicationQuit != null)
                ApplicationQuit.Invoke();
        }
    }
}
