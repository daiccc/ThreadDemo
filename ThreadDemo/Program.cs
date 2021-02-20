using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Program
    {
        #region 创建/启用线程

        //注意：创建Thread的实例之后，需要手动调用它的Start方法将其启动；但是对于Task来说，StartNew和Run的同时，既会从线程池中调用线程，并且会立即启动它。
        //static void Main(string[] args)
        //{
        //    new Thread(Go).Start();  // .Net 1.0 开始就有了
        //    Task.Factory.StartNew(Go);  // .Net 4.0 引入了TPL
        //    Task.Run(new Action(Go));  // .Net 4.5/c# 5.0 新增了一个Run方法
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

        //试想一下，如果有大量的任务需要处理，例如网站后台对于HTTP请求的处理，那是不是要对每一个请求创建一个后台线程呢？显然不合适，这会占用大量内存，而且频繁地创建的过程也会严重影响速度，那怎么办呢？线程池就是为了解决这一问题，把创建的线程存起来，形成一个线程池(里面有多个线程)，当要处理任务时，若线程池中有空闲线程(前一个任务执行完成后，线程不会被回收，会被设置为空闲状态)，则直接调用线程池中的线程执行(例asp.net处理机制中的Application对象)
        //static void Main()
        //{
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        ThreadPool.QueueUserWorkItem(m =>
        //        {
        //            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        //        });
        //    }

        //    Console.ReadLine();
        //}

        #endregion

        #region Task

        //Task是.NET4.0加入的，跟线程池ThreadPool的功能类似，用Task开启新任务时，会从线程池中调用线程，而Thread每次实例化都会创建一个新的线程。
        //static void Main()
        //{
        //    Console.WriteLine("主线程启动");
        //    Console.WriteLine("主线程运行中..1..");
        //    //Task.Run启动一个线程
        //    //Task启动的是后台线程，要在主线程中等待后台线程执行完毕，可以调用Wait方法
        //    Task task = Task.Run(() =>
        //    {
        //        Console.WriteLine("Task启动");
        //        Thread.Sleep(2000);
        //        Console.WriteLine("Task结束");
        //    });
        //    Console.WriteLine("主线程运行中..2..");
        //    Thread.Sleep(1000);
        //    Console.WriteLine("主线程运行中..3..");
        //    task.Wait();
        //    Console.WriteLine("主线程运行中..4..");
        //    Console.WriteLine("主线程结束");
        //}

        //开启新任务的方法：Task.Run() 或者Task.Factory.StartNew()，开启的是后台线程
        //要在主线程中等待后台线程执行完毕，可以使用Wait方法(会以同步的方式来执行)。不用Wait则会以异步的方式来执行。

        //比较一下Task和Thread：
        //static void Main()
        //{
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        new Thread(Go1).Start();
        //    }

        //    for (int i = 1; i <= 10; i++)
        //    {
        //        Task.Run(() => { Go2(); });
        //    }

        //    Console.ReadLine();
        //}

        //static void Go1()
        //{
        //    Console.WriteLine($"Thread Id：{Thread.CurrentThread.ManagedThreadId}");
        //}

        //static void Go2()
        //{
        //    Console.WriteLine($"Task调用Thread Id：{Thread.CurrentThread.ManagedThreadId}");
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

        #region Task<TResult>

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

        #region async & await

        //async用来修饰方法，表明这个方法是异步的，声明的方法的返回类型必须为：void、Task或Task<TResult>
        //await必须用来修饰Task或Task<TResult>，而且只能出现在已经用async关键字修饰的异步方法中。通常情况下，async/await成对出现才有意义
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

        #region await后的的执行顺序 

        //await 不会开启新的线程，当前线程会一直往下走直到遇到真正的Async方法（比如说HttpClient.GetStringAsync），这个方法的内部会用Task.Run或者Task.Factory.StartNew 去开启线程。也就是如果方法不是.NET为我们提供的Async方法，我们需要自己创建Task，才会真正的去创建线程。
        //static void Main()
        //{
        //    Console.WriteLine($"当前线程Id: {Thread.CurrentThread.ManagedThreadId} \r\n");
        //    Test();  //控制台的入口方法不支持async，所以无法直接调用GetName方法。
        //    Console.ReadLine();
        //}

        //static async Task Test()
        //{
        //    Console.WriteLine($"GetName执行前线程Id：{Thread.CurrentThread.ManagedThreadId} \r\n");
        //    var name = GetName();  // 这里没有用await，所以下面的代码可以执行
        //    //但是如果上面是await GetName()，下面的代码就不会立即执行，输出结果就不一样了
        //    Console.WriteLine("GetName执行结束 \r\n");
        //    Console.WriteLine($"GetName返回结果：{await name} \r\n");
        //}

        //static async Task<string> GetName()
        //{
        //    //这里还是主线程
        //    Console.WriteLine($"Task.Run之前，当前线程Id：{Thread.CurrentThread.ManagedThreadId} \r\n");
        //    return await Task.Run(() =>
        //    {
        //        Thread.Sleep(1000);
        //        Console.WriteLine($"GetName的线程Id：{Thread.CurrentThread.ManagedThreadId} \r\n");
        //        return "张三";
        //    });
        //}

        #endregion

        #region 只有async方法在调用前才能加await？

        //答案很明显：await并不是针对于async的方法，而是针对async方法所返回给我们的Task，这也是为什么所有的async方法都必须返回给我们Task。所以我们同样可以在Task前面也加上await关键字，这样做实际上是告诉编译器我需要等这个Task的返回值或者等这个Task执行完毕之后才能继续往下走。
        //static void Main()
        //{
        //    Test();
        //    Console.ReadLine();
        //}

        //static async void Test()
        //{
        //    Task<string> task = Task.Run(() =>
        //    {
        //        Thread.Sleep(2000);

        //        return "Hello Word!";
        //    });

        //    string res = await task;
        //    Console.WriteLine(res);
        //}

        #endregion

        #region 不用await关键字来获取Task执行完毕

        //不用await关键字来获取Task执行完毕
        //static void Main()
        //{
        //    var task = Task.Run(() =>
        //    {
        //        return GetName();
        //    });

        //    task.GetAwaiter().OnCompleted(() =>
        //    {
        //        var name = task.Result;
        //        Console.WriteLine($"获取到的名字是{name}");
        //    });

        //    Console.WriteLine("主线程执行完毕！");
        //    Console.ReadLine();
        //}

        //static string GetName()
        //{
        //    Console.WriteLine("另一个线程在获取名字");
        //    Thread.Sleep(2000);
        //    return "张三";
        //}

        #endregion

        #region Task.GetAwaiter()和await Task 的区别

        //加上await关键字之后，后面的代码会被挂起等待，直到task执行完毕有返回值的时候才会继续向下执行，这一段时间主线程会处于挂起状态。
        //static void Main()
        //{
        //    Console.WriteLine("主线程执行中...");
        //    var res = Test().Result;
        //    Console.WriteLine($"返回的结果是：{res}");
        //    Console.WriteLine("主线程执行完毕！");
        //}

        //static async Task<string> Test()
        //{
        //    Console.WriteLine("Test线程执行中...");
        //    Task<string> task = Task.Run(() =>
        //    {
        //        Thread.Sleep(2000);
        //        return "张三";
        //    });
        //    var res = await task;
        //    return res;
        //}

        //GetAwaiter方法会返回一个awaitable的对象（继承了INotifyCompletion.OnCompleted方法）我们只是传递了一个委托进去，等task完成了就会执行这个委托，但是并不会影响主线程，下面的代码会立即执行。这也是为什么我们结果里面第一句话会是 “主线程执行完毕”！
        //static void Main()
        //{
        //    Console.WriteLine("主线程运行中..1..");
        //    var task = Task.Run(() =>
        //    {
        //        Console.WriteLine("主线程运行中..4..");
        //        return GetName();
        //    });
        //    Console.WriteLine("主线程运行中..2..");
        //    task.GetAwaiter().OnCompleted(() =>
        //    {
        //        Console.WriteLine("主线程运行中..5..");
        //        var name = task.Result;
        //        Console.WriteLine("主线程运行中..6..");
        //        Console.WriteLine($"获取到的名字是{name}");
        //        Console.WriteLine("主线程运行中..7..");
        //    });
        //    Console.WriteLine("主线程运行中..3..");
        //    Console.WriteLine("主线程执行完毕！");
        //    Console.ReadLine();
        //}

        //static string GetName()
        //{
        //    Console.WriteLine("另一个线程在获取名字");
        //    Thread.Sleep(2000);
        //    return "张三";
        //}

        #endregion

        #region Task如何让主线程挂起等待？

        //Task.GetAwait()方法会给我们返回一个awaitable的对象，通过调用这个对象的GetResult方法就会挂起主线程，当然也不是所有的情况都会挂起。还记得我们Task的特性么？ 在一开始的时候就启动了另一个线程去执行这个Task，当我们调用它的结果的时候如果这个Task已经执行完毕，主线程是不用等待可以直接拿其结果的，如果没有执行完毕那主线程就得挂起等待了
        //static void Main()
        //{
        //    Console.WriteLine("主线程正在运行中...");

        //    var task = Task.Run(() =>
        //    {
        //        return GetName();
        //    });

        //    var name = task.GetAwaiter().GetResult();

        //    Console.WriteLine($"获取到的名字是：{name}");

        //    Console.WriteLine("主线程执行完毕！");
        //}

        //static string GetName()
        //{
        //    Console.WriteLine("另一个线程正在获取名字...");
        //    Thread.Sleep(2000);
        //    return "张三";
        //}

        #endregion

        #region await实质是在调用awaitable对象的GetResult方法

        //static void Main()
        //{
        //    Console.WriteLine("主线程正在运行");

        //    Test();

        //    Console.ReadLine();
        //}

        //static async void Test()
        //{
        //    Task<string> task = Task.Run(() =>
        //    {
        //        Console.WriteLine("另一个线程正在运行...");  // 这句话只会被执行一次
        //        Thread.Sleep(2000);
        //        return "哈哈哈";
        //    });

        //    var res1 = task.GetAwaiter().GetResult();  // 这里主线程会挂起等待，直到task执行完成拿到结果

        //    var res2 = await task;

        //    Console.WriteLine($"结果：{res1}--{res2}--{task.Result}");  // res2 和 task.Result 这里不会挂起等待，因为task已经执行完了，我们可以直接拿到结果
        //    Console.WriteLine("主线程运行完毕！");
        //}

        #endregion

        #region Task.ContinueWith(连续的任务)

        static void Main()
        {
            Console.WriteLine("主程序开始------------------");

            Task task1 = Task.Run(() =>
            {
                Go1();
            });

            var task2 = task1.ContinueWith(t => { Go2(); });
            var task3 = task2.ContinueWith(t => { Go3(); });

            Console.WriteLine("主程序结束------------------");
            Console.ReadLine();
        }

        static void Go1()
        {
            Thread.Sleep(1000);
            Console.WriteLine("任务一执行中...");
        }

        static void Go2()
        {
            Thread.Sleep(1000);
            Console.WriteLine("任务二执行中...");
        }

        static void Go3()
        {
            Thread.Sleep(1000);
            Console.WriteLine("任务三执行中...");
        }

        #endregion
    }
}
