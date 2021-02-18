using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Program
    {
        #region 创建线程

        //注意：创建Thread的实例之后，需要手动调用它的Start方法将其启动；但是对于Task来说，StartNew和Run的同时，既会创建新的线程，并且会立即启动它。
        //static void Main(string[] args)
        //{
        //    new Thread(Go).Start();  // .Net 1.0 开始就有了
        //    Task.Factory.StartNew(Go);  // .Net 4.0 引入了TPL
        //    Task.Run(new Action(Go));  // .Net 4.5 新增了一个Run方法
        //}

        //public static void Go()
        //{
        //    Console.WriteLine("Hello World!");
        //}

        #endregion

        #region 线程池

        //注意：创建Thread的实例之后，需要手动调用它的Start方法将其启动；但是对于Task来说，StartNew和Run的同时，既会创建新的线程，并且会立即启动它。
        //static void Main(string[] args)
        //{
        //    Console.WriteLine($"我是主线程：Thread Id {Thread.CurrentThread.ManagedThreadId}");

        //    ThreadPool.QueueUserWorkItem(Go);

        //    Console.ReadLine();
        //}

        //public static void Go(object data)
        //{
        //    Console.WriteLine("Hello Word!");
        //}

        #endregion

        #region 传入参数

        //static void Main(string[] args)
        //{ 
        //    new Thread(Go1).Start("111");  // 没有匿名委托之前，我们只能这样传入一个object的参数

        //    new Thread(delegate ()  //有了匿名委托。。
        //    {
        //        Go2("111", "222", "333");
        //    }).Start();

        //    new Thread(() =>  //匿名委托 + Lamada
        //    {
        //        Go2("444", "555", "666");
        //    }).Start();

        //    Task.Run(() =>
        //    {
        //        Go2("777", "888", "999");
        //    });
        //}

        //public static void Go1(object parm)
        //{
        //    Console.WriteLine($"传入的参数：{parm}");
        //}

        //public static void Go2(string p1, string p2, string p3)
        //{
        //    Console.WriteLine($"传入的参数：{p1}  {p2}  {p3}");
        //}

        #endregion

        #region 返回值

        //Thread是不能返回值的，但是作为更高级的Task当然要弥补一下这个功能。
        //static void Main(string[] args)
        //{
        //    var GoResult = Task.Run(() => { return ReturnGoResult(); });

        //    Console.WriteLine($"ReturnGoResult 线程返回的结果是：{GoResult.Result}");
        //}

        //public static Task<string> ReturnGoResult()
        //{
        //    return Task.Run(() =>
        //    {
        //        Thread.Sleep(1000);

        //        return Go();
        //    });
        //}

        //public static string Go()
        //{
        //    return "Hello Word!";
        //}

        #endregion

        #region 共享数据

        //private static bool _done = true;
        //static void Main(string[] args)
        //{
        //    Task.Run(() => { Go("1"); });

        //    Task.Run(() => { Go("2"); });

        //    Console.ReadLine();
        //}

        //public static void Go(string parm)
        //{
        //    if (_done)
        //    {
        //        _done = false;

        //        Console.WriteLine($"线程{parm}运行中...");
        //    }
        //}

        #endregion

        #region 线程安全

        //private static bool _done = true;
        //static void Main(string[] args)
        //{
        //    Task.Run(() => { Go("1"); });

        //    Task.Run(() => { Go("2"); });

        //    Console.ReadLine();
        //}

        //public static void Go(string parm)
        //{
        //    if (_done)
        //    {
        //        Console.WriteLine($"线程{parm}运行中...");

        //        _done = false;
        //    }
        //}

        #endregion

        #region 锁

        //独占锁
        //private static bool _done = true;
        //private static object _lock = new object();
        //static void Main(string[] args)
        //{
        //    Task.Run(() => { Go("1"); });

        //    Task.Run(() => { Go("2"); });

        //    Console.ReadLine();
        //}

        //public static void Go(string parm)
        //{
        //    lock (_lock)
        //    {
        //        if (_done)
        //        {
        //            Console.WriteLine($"线程{parm}运行中...");

        //            _done = false;
        //        }
        //    }
        //}

        #endregion

        #region Semaphore 信号量

        //static SemaphoreSlim _sem = new SemaphoreSlim(3);  // 限制同时访问的线程数量是3
        //static void Main()
        //{
        //    for (int i = 1; i <= 5; i++) new Thread(Enter).Start(i);

        //    Console.ReadLine();
        //}

        //static void Enter(object id)
        //{
        //    Console.WriteLine($"{id}  开始排队...");
        //    _sem.Wait();
        //    Console.WriteLine($"{id}  开始执行...");
        //    Thread.Sleep(500);
        //    Console.WriteLine($"{id}  执行结束！");
        //    _sem.Release();
        //}

        #endregion

        #region 异常处理

        // --- Thread ---
        //public static void Main()
        //{
        //    try
        //    {
        //        new Thread(Go).Start();
        //    }
        //    catch (Exception)
        //    {
        //        // 其它线程里面的异常，这里面是捕获不到的。
        //        Console.WriteLine("Exception!");
        //    }
        //}

        //static void Go()
        //{
        //    throw null;
        //}

        // --- Task ---
        //public static void Main()
        //{
        //    try
        //    {
        //        var task1 = Task.Run(() => { Go1(); });
        //        task1.Wait();  // 调用这句话之后，主线程才能捕获Task里面的异常。

        //        var task2 = Task.Run(() => { return Go2(); });
        //        var result = task2.Result;  // 对于有返回值的Task，接收了它的返回值就不用再调用Wait方法了。
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Exception!");
        //    }
        //}

        //static void Go1() { throw null; }
        //static string Go2() { throw null; }

        #endregion

        #region Async & Await

        //static void Main()
        //{
        //    //控制台的入口方法不支持async，所以无法直接调用GetName方法。
        //    Test();

        //    Console.WriteLine($"Current Thread Id: {Thread.CurrentThread.ManagedThreadId}");

        //    Console.ReadLine();
        //}

        //static async Task Test()
        //{
        //    await GetName();
        //}

        //static async Task GetName()
        //{
        //    //返回值前面加async之后，方法里面就可以用await。
        //    await Task.Delay(1000);  // Delay方法来自于.net 4.5；Task.Delay()是异步编程提供的延迟方法，如果你想延迟1秒，可以Task.Delay(1000);
        //    //当Task.Delay(1000)执行后，会异步延迟delay的时间,在延迟的同时，执行下方的Console；
        //    //当这行代码前 + await，会等待异步延迟的执行结束后，执行下方的Console；
        //    Console.WriteLine($"Current Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        //}

        #endregion

        #region Await的原形

        static void Main()
        {
            Console.WriteLine($"当前线程Id: {Thread.CurrentThread.ManagedThreadId} \r\n");
            Test();  //控制台的入口方法不支持async，所以无法直接调用GetName方法。
            Console.ReadLine();
        }

        static async Task Test()
        {
            Console.WriteLine($"GetName执行前线程Id：{Thread.CurrentThread.ManagedThreadId} \r\n");
            var name = GetName();  // 这里没有用await，所以下面的代码可以执行
            //但是如果上面是await GetName()，下面的代码就不会立即执行，输出结果就不一样了
            Console.WriteLine("GetName执行结束 \r\n");
            Console.WriteLine($"GetName返回结果：{await name} \r\n");
        }

        static async Task<string> GetName()
        {
            //这里还是主线程
            Console.WriteLine($"Task.Run之前，当前线程Id：{Thread.CurrentThread.ManagedThreadId} \r\n");
            return await Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"GetName的线程Id：{Thread.CurrentThread.ManagedThreadId} \r\n");
                return "张三";
            });
        }

        #endregion
    }
}
