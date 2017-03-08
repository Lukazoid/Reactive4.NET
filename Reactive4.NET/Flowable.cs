﻿using Reactive4.NET.operators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reactive.Streams;

namespace Reactive4.NET
{
    public static class Flowable
    {
        public static int BufferSize()
        {
            return 128;
        }

        // ********************************************************************************
        // Interop methods
        // ********************************************************************************

        public static IFlowable<T> ToFlowable<T>(this IPublisher<T> publisher)
        {
            if (publisher is IFlowable<T> f)
            {
                return f;
            }
            return new FlowableFromPublisher<T>(publisher);
        }

        public static IFlowable<T> FromPublisher<T>(IPublisher<T> publisher)
        {
            return publisher.ToFlowable();
        }

        public static IFlowable<T> ToFlowable<T>(this IObservable<T> source, BackpressureStrategy strategy)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> FromObservable<T>(IObservable<T> source, BackpressureStrategy strategy)
        {
            return source.ToFlowable(strategy);
        }

        public static IFlowable<T> ToFlowable<T>(this Task<T> task)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> FromTask<T>(Task<T> task)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<object> ToFlowable(this Task task)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<object> FromTask(this Task task)
        {
            throw new NotImplementedException();
        }

        public static IObservable<T> ToObservable<T>(this IFlowable<T> source)
        {
            throw new NotImplementedException();
        }

        // ********************************************************************************
        // Factory methods
        // ********************************************************************************

        public static IFlowable<T> Just<T>(T item)
        {
            return new FlowableJust<T>(item);
        }

        public static IFlowable<T> RepeatItem<T>(T item)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Error<T>(Exception exception)
        {
            return new FlowableError<T>(exception);
        }

        public static IFlowable<T> Error<T>(Func<Exception> exception)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Empty<T>()
        {
            return FlowableEmpty<T>.Instance;
        }

        public static IFlowable<T> Never<T>()
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> FromFunction<T>(Func<T> function)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> RepeatFunction<T>(Func<T> function)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Create<T>(Action<IFlowableEmitter<T>> emitter)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Generate<T>(Action<IGeneratorEmitter<T>> emitter)
        {
            return Generate<T, object>(() => null, (s, e) => { emitter(e); return null; }, s => { });
        }

        public static IFlowable<T> Generate<T, S>(Func<S> stateFactory, Action<S, IGeneratorEmitter<T>> emitter)
        {
            return Generate<T, S>(stateFactory, (s, e) => { emitter(s, e); return s; }, s => { });
        }

        public static IFlowable<T> Generate<T, S>(Func<S> stateFactory, Action<S, IGeneratorEmitter<T>> emitter, Action<S> stateCleanup, bool eager = false)
        {
            return Generate<T, S>(stateFactory, (s, e) => { emitter(s, e); return s; }, stateCleanup, eager);
        }

        public static IFlowable<T> Generate<T, S>(Func<S> stateFactory, Func<S, IGeneratorEmitter<T>, S> emitter)
        {
            return Generate<T, S>(stateFactory, emitter, s => { });
        }

        public static IFlowable<T> Generate<T, S>(Func<S> stateFactory, Func<S, IGeneratorEmitter<T>, S> emitter, Action<S> stateCleanup, bool eager = false)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> FromArray<T>(params T[] items)
        {
            return new FlowableArray<T>(items);
        }

        public static IFlowable<T> FromEnumerable<T>(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<int> Range(int start, int count)
        {
            return new FlowableRange(start, start + count);
        }

        // ********************************************************************************
        // Multi-source factory methods
        // ********************************************************************************

        public static IFlowable<T> Amb<T>(params IPublisher<T>[] sources) {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Amb<T>(IEnumerable<IPublisher<T>> sources)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Concat<T>(params IPublisher<T>[] sources)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Concat<T>(IEnumerable<IPublisher<T>> sources)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Concat<T>(IPublisher<IPublisher<T>> sources)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> ConcatEager<T>(params IPublisher<T>[] sources)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> ConcatEager<T>(IEnumerable<IPublisher<T>> sources)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> ConcatEager<T>(IPublisher<IPublisher<T>> sources)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Merge<T>(params IPublisher<T>[] sources)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Merge<T>(IEnumerable<IPublisher<T>> sources, int maxConcurrency)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Merge<T>(IPublisher<IPublisher<T>> sources, int maxConcurrency)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Flatten<T>(this IPublisher<IPublisher<T>> sources)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Flatten<T>(this IPublisher<IPublisher<T>> sources, int maxConcurrency)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> CombineLatest<T, R>(Func<T[], R> combiner, params IPublisher<T>[] sources)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> CombineLatest<T, R>(IEnumerable<IPublisher<T>> sources, Func<T[], R> combiner)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> CombineLatest<T1, T2, R>(IPublisher<T1> source1, IPublisher<T2> source2, Func<T1, T2, R> combiner)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> CombineLatest<T1, T2, T3, R>(IPublisher<T1> source1, IPublisher<T2> source2,
            IPublisher<T3> source3, Func<T1, T2, T3, R> combiner)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> CombineLatest<T1, T2, T3, T4, R>(IPublisher<T1> source1, IPublisher<T2> source2,
            IPublisher<T3> source3, IPublisher<T4> source4, Func<T1, T2, T3, T4, R> combiner)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> Zip<T, R>(Func<T[], R> zipper, params IPublisher<T>[] sources)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> Zip<T, R>(IEnumerable<IPublisher<T>> sources, Func<T[], R> zipper)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> Zip<T1, T2, R>(IPublisher<T1> source1, IPublisher<T2> source2, Func<T1, T2, R> zipper)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> Zip<T1, T2, T3, R>(IPublisher<T1> source1, IPublisher<T2> source2, 
            IPublisher<T3> source3, Func<T1, T2, T3, R> zipper)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> Zip<T1, T2, T3, T4, R>(IPublisher<T1> source1, IPublisher<T2> source2,
            IPublisher<T3> source3, IPublisher<T4> source4, Func<T1, T2, T3, T4, R> zipper)
        {
            throw new NotImplementedException();
        }

        // ********************************************************************************
        // Instance operators
        // ********************************************************************************

        public static IParallelFlowable<T> Parallel<T>(this IFlowable<T> source)
        {
            return Parallel(source, Environment.ProcessorCount, BufferSize());
        }

        public static IParallelFlowable<T> Parallel<T>(this IFlowable<T> source, int parallelism)
        {
            return Parallel(source, parallelism, BufferSize());
        }

        public static IParallelFlowable<T> Parallel<T>(this IFlowable<T> source, int parallelism, int bufferSize)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> Map<T, R>(this IFlowable<T> source, Func<T, R> mapper)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> MapAsync<T, R>(this IFlowable<T> source, Func<T, IPublisher<R>> mapper)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> MapAsync<T, U, R>(this IFlowable<T> source, Func<T, IPublisher<U>> mapper, Func<T, U, R> resultMapper)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> Filter<T>(this IFlowable<T> source, Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> FilterAsync<T>(this IFlowable<T> source, Func<T, IPublisher<bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> Take<T, R>(this IFlowable<T> source, long n)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> Skip<T, R>(this IFlowable<T> source, long n)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> TakeLast<T, R>(this IFlowable<T> source, long n)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> SkipLast<T, R>(this IFlowable<T> source, long n)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<C> Collect<T, C>(this IFlowable<T> source, Func<C> collectionSupplier, Action<C, T> collector)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> Reduce<T, R>(this IFlowable<T> source, Func<R> initialSupplier, Func<R, T, R> reducer)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<IList<T>> ToList<T>(this IFlowable<T> source, int capacityHint = 10)
        {
            return Collect(source, () => new List<T>(capacityHint), (a, b) => a.Add(b));
        }

        public static IFlowable<int> SumInt(this IFlowable<int> source)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<long> SumLong(this IFlowable<long> source)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<int> MaxInt(this IFlowable<int> source)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<long> MaxLong(this IFlowable<long> source)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<int> MinInt(this IFlowable<int> source)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<long> MinLong(this IFlowable<long> source)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<T> IgnoreElements<T>(this IFlowable<T> source)
        {
            throw new NotImplementedException();
        }

        public static IFlowable<R> FlatMap<T, R>(this IFlowable<T> source, Func<T, IPublisher<R>> mapper)
        {
            return FlatMap(source, mapper, BufferSize(), BufferSize());
        }

        public static IFlowable<R> FlatMap<T, R>(this IFlowable<T> source, Func<T, IPublisher<R>> mapper, int maxConcurrency)
        {
            return FlatMap(source, mapper, maxConcurrency, BufferSize());
        }

        public static IFlowable<R> FlatMap<T, R>(this IFlowable<T> source, Func<T, IPublisher<R>> mapper, int maxConcurrency, int bufferSize)
        {
            return new FlowableFlatMap<T, R>(source, mapper, maxConcurrency, bufferSize);
        }

        // ********************************************************************************
        // Consumer methods
        // ********************************************************************************

        public static IDisposable Subscribe<T>(this IFlowable<T> source)
        {
            throw new NotImplementedException();
        }

        public static IDisposable Subscribe<T>(this IFlowable<T> source, Action<T> onNext)
        {
            throw new NotImplementedException();
        }

        public static IDisposable Subscribe<T>(this IFlowable<T> source, Action<T> onNext, Action<T> onError)
        {
            throw new NotImplementedException();
        }

        public static IDisposable Subscribe<T>(this IFlowable<T> source, Action<T> onNext, Action<T> onError, Action onComplete)
        {
            throw new NotImplementedException();
        }

        public static Task<T> FirstTask<T>(this IFlowable<T> source)
        {
            throw new NotImplementedException();
        }

        public static Task<T> LastTask<T>(this IFlowable<T> source)
        {
            throw new NotImplementedException();
        }

        public static Task IgnoreElementsTask<T>(this IFlowable<T> source)
        {
            throw new NotImplementedException();
        }

        // ********************************************************************************
        // Blocking operators
        // ********************************************************************************

        public static T BlockingFirst<T>(this IFlowable<T> source)
        {
            throw new NotImplementedException();
        }

        public static T BlockingLast<T>(this IFlowable<T> source)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<T> BlockingEnumerable<T>(this IFlowable<T> source)
        {
            throw new NotImplementedException();
        }

        public static IDisposable BlockingSubscribe<T>(this IFlowable<T> source)
        {
            throw new NotImplementedException();
        }

        public static IDisposable BlockingSubscribe<T>(this IFlowable<T> source, Action<T> onNext)
        {
            throw new NotImplementedException();
        }

        public static IDisposable BlockingSubscribe<T>(this IFlowable<T> source, Action<T> onNext, Action<T> onError)
        {
            throw new NotImplementedException();
        }

        public static IDisposable BlockingSubscribe<T>(this IFlowable<T> source, Action<T> onNext, Action<T> onError, Action onComplete)
        {
            throw new NotImplementedException();
        }

        // ********************************************************************************
        // Test methods
        // ********************************************************************************

        public static TestSubscriber<T> Test<T>(this IFlowable<T> source, long initialRequest = long.MaxValue, bool cancel = false)
        {
            var ts = new TestSubscriber<T>(initialRequest);
            if (cancel)
            {
                ts.Cancel();
            }
            source.Subscribe(ts);
            return ts;
        }
    }
}