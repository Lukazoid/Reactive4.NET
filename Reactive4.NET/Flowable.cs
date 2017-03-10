﻿using Reactive4.NET.operators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reactive.Streams;
using Reactive4.NET.subscribers;

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
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> FromObservable<T>(IObservable<T> source, BackpressureStrategy strategy)
        {
            return source.ToFlowable(strategy);
        }

        public static IFlowable<T> ToFlowable<T>(this Task<T> task)
        {
            return new FlowableFromTask<T>(task);
        }

        public static IFlowable<T> FromTask<T>(Task<T> task)
        {
            return task.ToFlowable();
        }

        public static IFlowable<object> ToFlowable(this Task task)
        {
            return new FlowableFromTaskVoid(task);
        }

        public static IFlowable<object> FromTask(this Task task)
        {
            return task.ToFlowable();
        }

        public static IObservable<T> ToObservable<T>(this IFlowable<T> source)
        {
            return new FlowableToObservable<T>(source);
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
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Error<T>(Exception exception)
        {
            return new FlowableError<T>(exception);
        }

        public static IFlowable<T> Error<T>(Func<Exception> errorSupplier)
        {
            return new FlowableErrorSupplier<T>(errorSupplier);
        }

        public static IFlowable<T> Empty<T>()
        {
            return FlowableEmpty<T>.Instance;
        }

        public static IFlowable<T> Never<T>()
        {
            return FlowableNever<T>.Instance;
        }

        public static IFlowable<T> FromFunction<T>(Func<T> function)
        {
            return new FlowableFromFunction<T>(function);
        }

        public static IFlowable<T> RepeatFunction<T>(Func<T> function)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Create<T>(Action<IFlowableEmitter<T>> emitter)
        {
            // TODO implement
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
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> FromArray<T>(params T[] items)
        {
            return new FlowableArray<T>(items);
        }

        public static IFlowable<T> FromEnumerable<T>(IEnumerable<T> items)
        {
            return new FlowableEnumerable<T>(items);
        }

        public static IFlowable<int> Range(int start, int count)
        {
            return new FlowableRange(start, start + count);
        }

        public static IFlowable<T> Defer<T>(Func<IPublisher<T>> supplier)
        {
            return new FlowableDefer<T>(supplier);
        }

        public static IFlowable<long> Timer()
        {
            return Timer(Executors.Computation);
        }

        public static IFlowable<long> Timer(IExecutorService executor)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<long> Interval(long period)
        {
            return Interval(period, period, Executors.Computation);
        }

        public static IFlowable<long> Interval(long initialDelay, long period)
        {
            return Interval(initialDelay, period, Executors.Computation);
        }

        public static IFlowable<long> Interval(long period, IExecutorService executor)
        {
            return Interval(period, period, executor);
        }

        public static IFlowable<long> Interval(long initialDelay, long period, IExecutorService executor)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        // ********************************************************************************
        // Multi-source factory methods
        // ********************************************************************************

        public static IFlowable<T> Amb<T>(params IPublisher<T>[] sources) {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Amb<T>(IEnumerable<IPublisher<T>> sources)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Concat<T>(params IPublisher<T>[] sources)
        {
            return new FlowableConcatArray<T>(sources);
        }

        public static IFlowable<T> Concat<T>(IEnumerable<IPublisher<T>> sources)
        {
            return new FlowableConcatEnumerable<T>(sources);
        }

        public static IFlowable<T> Concat<T>(this IPublisher<IPublisher<T>> sources)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> ConcatEager<T>(params IPublisher<T>[] sources)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> ConcatEager<T>(IEnumerable<IPublisher<T>> sources)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> ConcatEager<T>(this IPublisher<IPublisher<T>> sources)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Merge<T>(params IPublisher<T>[] sources)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Merge<T>(IEnumerable<IPublisher<T>> sources, int maxConcurrency)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Merge<T>(this IPublisher<IPublisher<T>> sources)
        {
            return Merge(sources, BufferSize(), BufferSize());
        }

        public static IFlowable<T> Merge<T>(this IPublisher<IPublisher<T>> sources, int maxConcurrency)
        {
            return Merge(sources, maxConcurrency, BufferSize());
        }

        public static IFlowable<T> Merge<T>(this IPublisher<IPublisher<T>> sources, int maxConcurrency, int bufferSize)
        {
            return new FlowableFlatMapPublisher<IPublisher<T>, T>(sources, v => v, maxConcurrency, bufferSize);
        }

        public static IFlowable<R> CombineLatest<T, R>(Func<T[], R> combiner, params IPublisher<T>[] sources)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> CombineLatest<T, R>(IEnumerable<IPublisher<T>> sources, Func<T[], R> combiner)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> CombineLatest<T1, T2, R>(IPublisher<T1> source1, IPublisher<T2> source2, Func<T1, T2, R> combiner)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> CombineLatest<T1, T2, T3, R>(IPublisher<T1> source1, IPublisher<T2> source2,
            IPublisher<T3> source3, Func<T1, T2, T3, R> combiner)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> CombineLatest<T1, T2, T3, T4, R>(IPublisher<T1> source1, IPublisher<T2> source2,
            IPublisher<T3> source3, IPublisher<T4> source4, Func<T1, T2, T3, T4, R> combiner)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> Zip<T, R>(Func<T[], R> zipper, params IPublisher<T>[] sources)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> Zip<T, R>(IEnumerable<IPublisher<T>> sources, Func<T[], R> zipper)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> Zip<T1, T2, R>(IPublisher<T1> source1, IPublisher<T2> source2, Func<T1, T2, R> zipper)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> Zip<T1, T2, T3, R>(IPublisher<T1> source1, IPublisher<T2> source2, 
            IPublisher<T3> source3, Func<T1, T2, T3, R> zipper)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> Zip<T1, T2, T3, T4, R>(IPublisher<T1> source1, IPublisher<T2> source2,
            IPublisher<T3> source3, IPublisher<T4> source4, Func<T1, T2, T3, T4, R> zipper)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> SwitchOnNext<T>(IPublisher<IPublisher<T>> sources)
        {
            // TODO implement
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
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> Map<T, R>(this IFlowable<T> source, Func<T, R> mapper)
        {
            return new FlowableMap<T, R>(source, mapper);
        }

        public static IFlowable<R> MapAsync<T, R>(this IFlowable<T> source, Func<T, IPublisher<R>> mapper)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> MapAsync<T, U, R>(this IFlowable<T> source, Func<T, IPublisher<U>> mapper, Func<T, U, R> resultMapper)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Filter<T>(this IFlowable<T> source, Func<T, bool> predicate)
        {
            return new FlowableFilter<T>(source, predicate);
        }

        public static IFlowable<T> FilterAsync<T>(this IFlowable<T> source, Func<T, IPublisher<bool>> predicate)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Take<T>(this IFlowable<T> source, long n, bool limitRequest = false)
        {
            return new FlowableTake<T>(source, n, limitRequest);
        }

        public static IFlowable<T> Skip<T>(this IFlowable<T> source, long n)
        {
            return new FlowableSkip<T>(source, n);
        }

        public static IFlowable<R> TakeLast<T, R>(this IFlowable<T> source, long n)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> SkipLast<T, R>(this IFlowable<T> source, long n)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<C> Collect<T, C>(this IFlowable<T> source, Func<C> collectionSupplier, Action<C, T> collector)
        {
            return new FlowableCollect<T, C>(source, collectionSupplier, collector);
        }

        public static IFlowable<T> Reduce<T>(this IFlowable<T> source, Func<T, T, T> reducer)
        {
            return new FlowableReducePlain<T>(source, reducer);
        }

        public static IFlowable<R> Reduce<T, R>(this IFlowable<T> source, Func<R> initialSupplier, Func<R, T, R> reducer)
        {
            return new FlowableReduce<T, R>(source, initialSupplier, reducer);
        }

        public static IFlowable<IList<T>> ToList<T>(this IFlowable<T> source, int capacityHint = 10)
        {
            return Collect(source, () => new List<T>(capacityHint), (a, b) => a.Add(b));
        }

        public static IFlowable<int> SumInt(this IFlowable<int> source)
        {
            return Reduce(source, (a, b) => a + b);
        }

        public static IFlowable<long> SumLong(this IFlowable<long> source)
        {
            return Reduce(source, (a, b) => a + b);
        }

        public static IFlowable<int> MaxInt(this IFlowable<int> source)
        {
            return Reduce(source, (a, b) => Math.Max(a, b));
        }

        public static IFlowable<T> Max<T>(this IFlowable<T> source, IComparer<T> comparer)
        {
            return Reduce(source, (a, b) => comparer.Compare(a, b) < 0 ? b : a);
        }

        public static IFlowable<long> MaxLong(this IFlowable<long> source)
        {
            return Reduce(source, (a, b) => Math.Max(a, b));
        }

        public static IFlowable<int> MinInt(this IFlowable<int> source)
        {
            return Reduce(source, (a, b) => Math.Min(a, b));
        }

        public static IFlowable<long> MinLong(this IFlowable<long> source)
        {
            return Reduce(source, (a, b) => Math.Min(a, b));
        }

        public static IFlowable<T> Min<T>(this IFlowable<T> source, IComparer<T> comparer)
        {
            return Reduce(source, (a, b) => comparer.Compare(a, b) < 0 ? a : b);
        }

        public static IFlowable<T> IgnoreElements<T>(this IFlowable<T> source)
        {
            return new FlowableIgnoreElements<T>(source);
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

        public static IFlowable<T> SubscribeOn<T>(this IFlowable<T> source, IExecutorService executor, bool requestOn = true)
        {
            return new FlowableSubscribeOn<T>(source, executor, requestOn);
        }

        public static IFlowable<T> ObserveOn<T>(this IFlowable<T> source, IExecutorService executor)
        {
            return ObserveOn(source, executor, BufferSize());
        }

        public static IFlowable<T> ObserveOn<T>(this IFlowable<T> source, IExecutorService executor, int bufferSize)
        {
            return new FlowableObserveOn<T>(source, executor, bufferSize);
        }

        public static IFlowable<T> RebatchRequests<T>(this IFlowable<T> source, int batchSize)
        {
            return ObserveOn(source, Executors.Immediate, batchSize);
        }

        public static IFlowable<T> Delay<T>(this IFlowable<T> source, TimeSpan delay)
        {
            return Delay(source, delay, Executors.Computation);
        }

        public static IFlowable<T> Delay<T>(this IFlowable<T> source, TimeSpan delay, IExecutorService executor)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> ConcatMap<T, R>(this IFlowable<T> source, Func<T, IPublisher<R>> mapper)
        {
            return ConcatMap(source, mapper, 2);
        }

        public static IFlowable<R> ConcatMap<T, R>(this IFlowable<T> source, Func<T, IPublisher<R>> mapper, int prefetch)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> ConcatMapEager<T, R>(this IFlowable<T> source, Func<T, IPublisher<R>> mapper)
        {
            return ConcatMapEager(source, mapper, BufferSize());
        }

        public static IFlowable<R> ConcatMapEager<T, R>(this IFlowable<T> source, Func<T, IPublisher<R>> mapper, int prefetch)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Hide<T>(this IFlowable<T> source)
        {
            return new FlowableHide<T>(source);
        }

        public static IFlowable<T> Distinct<T>(this IFlowable<T> source)
        {
            return Distinct(source, EqualityComparer<T>.Default);
        }

        public static IFlowable<T> Distinct<T>(this IFlowable<T> source, IEqualityComparer<T> comparer)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> DistinctUntilChanged<T>(this IFlowable<T> source)
        {
            return DistinctUntilChanged(source, EqualityComparer<T>.Default);
        }

        public static IFlowable<T> DistinctUntilChanged<T>(this IFlowable<T> source, IEqualityComparer<T> comparer)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> TakeUntil<T, U>(this IFlowable<T> source, IPublisher<U> other)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> SkipUntil<T, U>(this IFlowable<T> source, IPublisher<U> other)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> Lift<T, R>(this IFlowable<T> source, Func<IFlowableSubscriber<R>, IFlowableSubscriber<T>> onLift)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> Compose<T, R>(this IFlowable<T> source, Func<IFlowable<T>, IPublisher<R>> composer)
        {
            return composer(source).ToFlowable();
        }

        public static R To<T, R>(this IFlowable<T> source, Func<IFlowable<T>, R> converter)
        {
            return converter(source);
        }

        public static IFlowable<R> DeferComose<T, R>(this IFlowable<T> source, Func<IFlowable<T>, IPublisher<R>> composer)
        {
            return Defer(() => composer(source));
        }

        public static IFlowable<R> SwitchMap<T, R>(this IFlowable<T> source, Func<T, IPublisher<R>> mapper)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> DefaultIfEmpty<T>(this IFlowable<T> source, T defaultItem)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> SwitchIfEmpty<T>(this IFlowable<T> source, IPublisher<T> fallback)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> SwitchIfEmpty<T>(this IFlowable<T> source, params IPublisher<T>[] fallbacks)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> SwitchIfEmpty<T>(this IFlowable<T> source, IEnumerable<IPublisher<T>> fallbacks)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Repeat<T>(this IFlowable<T> source, long times = long.MaxValue)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Repeat<T>(this IFlowable<T> source, Func<bool> stop, long times = long.MaxValue)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> RepeatWhen<T, U>(this IFlowable<T> source, Func<IFlowable<object>, IPublisher<U>> handler)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Retry<T>(this IFlowable<T> source, long times = long.MaxValue)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Retry<T>(this IFlowable<T> source, Func<Exception, bool> predicate, long times = long.MaxValue)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> RetryWhen<T, U>(this IFlowable<T> source, Func<IFlowable<Exception>, IPublisher<U>> handler)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> OnErrorReturn<T>(this IFlowable<T> source, T fallbackItem)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> OnErrorComplete<T>(this IFlowable<T> source)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> OnErrorResumeNext<T>(this IFlowable<T> source, IPublisher<T> fallback)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> OnErrorResumeNext<T>(this IFlowable<T> source, Func<Exception, IPublisher<T>> handler)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Timeout<T>(this IFlowable<T> source, TimeSpan itemTimeout, IPublisher<T> fallback = null)
        {
            return Timeout<T>(source, itemTimeout, Executors.Computation, fallback);
        }

        public static IFlowable<T> Timeout<T>(this IFlowable<T> source, TimeSpan itemTimeout, IExecutorService executor, IPublisher<T> fallback = null)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Timeout<T>(this IFlowable<T> source, TimeSpan firstTimeout, TimeSpan itemTimeout, IPublisher<T> fallback = null)
        {
            return Timeout<T>(source, firstTimeout, itemTimeout, Executors.Computation, fallback);
        }

        public static IFlowable<T> Timeout<T>(this IFlowable<T> source, TimeSpan firstTimeout, TimeSpan itemTimeout, IExecutorService executor, IPublisher<T> fallback = null)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> OnBackpressureError<T>(this IFlowable<T> source)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> OnBackpressureDrop<T>(this IFlowable<T> source, Action<T> onDrop = null)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> OnBackpressureLatest<T>(this IFlowable<T> source)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> OnBackpressureBuffer<T>(this IFlowable<T> source)
        {
            return OnBackpressureBuffer(source, BufferSize(), BufferStrategy.ALL, null);
        }

        public static IFlowable<T> OnBackpressureBuffer<T>(this IFlowable<T> source, int capacityHint, BufferStrategy strategy = BufferStrategy.ALL, Action<T> onDrop = null)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<IGroupedFlowable<K, T>> GroupBy<T, K>(this IFlowable<T> source, Func<T, K> keyMapper)
        {
            return GroupBy<T, K, T>(source, keyMapper, v => v);
        }

        public static IFlowable<IGroupedFlowable<K, V>> GroupBy<T, K, V>(this IFlowable<T> source, Func<T, K> keyMapper, Func<T, V> valueMapper)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> WithLatestFrom<T, U, R>(this IFlowable<T> source, IPublisher<U> other, Func<T, U, R> combiner)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> WithLatestFrom<T, R>(this IFlowable<T> source, Func<T[], R> combiner, params IPublisher<T>[] others)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> WithLatestFrom<T, R>(this IFlowable<T> source, Func<T[], R> combiner, IEnumerable<IPublisher<T>> others)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Sample<T>(this IFlowable<T> source, TimeSpan period)
        {
            return Sample(source, period, Executors.Computation);
        }

        public static IFlowable<T> Sample<T>(this IFlowable<T> source, TimeSpan period, IExecutorService executor)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Sample<T, U>(this IFlowable<T> source, IPublisher<U> sampler)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Debounce<T>(this IFlowable<T> source, TimeSpan delay)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> ThrottleFirst<T>(this IFlowable<T> source, TimeSpan delay)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> ThrottleLast<T>(this IFlowable<T> source, TimeSpan delay)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> ThrottleWithTimeout<T>(this IFlowable<T> source, TimeSpan delay)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<IList<T>> Buffer<T>(this IFlowable<T> source, int size)
        {
            return Buffer(source, size, size, () => new List<T>());
        }

        public static IFlowable<C> Buffer<T, C>(this IFlowable<T> source, int size, Func<C> collectionSupplier) where C : ICollection<T>
        {
            return Buffer(source, size, size, collectionSupplier);
        }

        public static IFlowable<IList<T>> Buffer<T>(this IFlowable<T> source, int size, int skip)
        {
            return Buffer(source, size, skip, () => new List<T>());
        }

        public static IFlowable<C> Buffer<T, C>(this IFlowable<T> source, int size, int skip, Func<C> collectionSupplier) where C : ICollection<T>
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<IList<T>> Buffer<T, U>(this IFlowable<T> source, IPublisher<U> boundary)
        {
            return Buffer(source, boundary, () => new List<T>());
        }

        public static IFlowable<C> Buffer<T, U, C>(this IFlowable<T> source, IPublisher<U> boundary, Func<C> collectionSupplier) where C : ICollection<T>
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<IFlowable<T>> Window<T>(this IFlowable<T> source, int size)
        {
            return Window(source, size, size);
        }

        public static IFlowable<IFlowable<T>> Window<T>(this IFlowable<T> source, int size, int skip)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<IFlowable<T>> Window<T, U>(this IFlowable<T> source, IPublisher<U> boundary)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> Scan<T, R>(this IFlowable<T> source, Func<T, T, T> scanner)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> Scan<T, R>(this IFlowable<T> source, Func<R> initialSupplier, Func<R, T, R> scanner)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> AmbWith<T>(this IFlowable<T> source, IPublisher<T> other)
        {
            return Amb(source, other);
        }

        public static IFlowable<T> ConcatWith<T>(this IFlowable<T> source, IPublisher<T> other)
        {
            return Concat(source, other);
        }

        public static IFlowable<T> MergeWith<T>(this IFlowable<T> source, IPublisher<T> other)
        {
            return Merge(source, other);
        }

        public static IFlowable<R> ZipWith<T, U, R>(this IFlowable<T> source, IPublisher<U> other, Func<T, U, R> zipper)
        {
            return Zip(source, other, zipper);
        }

        // ********************************************************************************
        // IConnectableFlowable related
        // ********************************************************************************

        public static IConnectableFlowable<T> Publish<T>(this IFlowable<T> source)
        {
            return Publish(source, BufferSize());
        }

        public static IConnectableFlowable<T> Publish<T>(this IFlowable<T> source, int bufferSize)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<R> Publish<T, R>(this IFlowable<T> source, Func<IFlowable<T>, IPublisher<R>> handler)
        {
            return Publish(source, handler, BufferSize());
        }

        public static IFlowable<R> Publish<T, R>(this IFlowable<T> source, Func<IFlowable<T>, IPublisher<R>> handler, int bufferSize)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IConnectableFlowable<T> Replay<T>(this IFlowable<T> source)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IConnectableFlowable<R> Replay<T, R>(this IFlowable<T> source, Func<IFlowable<T>, IPublisher<R>> handler)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> AutoConnect<T>(this IConnectableFlowable<T> source, int count = 1, Action<IDisposable> onConnect = null)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> RefCount<T>(this IConnectableFlowable<T> source, int count = 1)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowableProcessor<T> RefCount<T>(this IFlowableProcessor<T> source, int count = 1)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowableProcessor<T> Serialize<T>(this IFlowableProcessor<T> source)
        {
            return new FlowableProcessorSerialize<T>(source);
        }

        // ********************************************************************************
        // State peeking methods
        // ********************************************************************************

        public static IFlowable<T> DoOnNext<T>(this IFlowable<T> source, Action<T> onNext)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> DoAfterNext<T>(this IFlowable<T> source, Action<T> onAfterNext)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> DoOnError<T>(this IFlowable<T> source, Action<Exception> onError)
        {
            // TODO implement
            throw new NotImplementedException();
        }


        public static IFlowable<T> DoOnComplete<T>(this IFlowable<T> source, Action onComplete)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> DoOnTerminated<T>(this IFlowable<T> source, Action onTerminated)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> DoAfterTerminated<T>(this IFlowable<T> source, Action onAfterTerminated)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> DoFinally<T>(this IFlowable<T> source, Action onFinally)
        {
            return new FlowableDoFinally<T>(source, onFinally);
        }

        public static IFlowable<T> DoOnSubscribe<T>(this IFlowable<T> source, Action<ISubscription> onSubscribe)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> DoOnRequest<T>(this IFlowable<T> source, Action<long> onRequest)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> DoOnCancel<T>(this IFlowable<T> source, Action onCancel)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static IFlowable<T> DoOnPoll<T>(this IFlowable<T> source, Action<bool, T> onPoll)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        // ********************************************************************************
        // Consumer methods
        // ********************************************************************************

        public static IDisposable Subscribe<T>(this IFlowable<T> source)
        {
            return Subscribe(source, v => { }, e => { }, () => { });
        }

        public static IDisposable Subscribe<T>(this IFlowable<T> source, Action<T> onNext)
        {
            return Subscribe(source, onNext, e => { }, () => { });
        }

        public static IDisposable Subscribe<T>(this IFlowable<T> source, Action<T> onNext, Action<Exception> onError)
        {
            return Subscribe(source, onNext, onError, () => { });
        }

        public static IDisposable Subscribe<T>(this IFlowable<T> source, Action<T> onNext, Action<Exception> onError, Action onComplete)
        {
            var s = new ActionSubscriber<T>(onNext, onError, onComplete);
            source.Subscribe(s);
            return s;
        }

        public static S SubscribeWith<T, S>(this IFlowable<T> source, S subscriber) where S : IFlowableSubscriber<T>
        {
            source.Subscribe(subscriber);
            return subscriber;
        }

        public static Task<T> FirstTask<T>(this IFlowable<T> source)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static Task<T> LastTask<T>(this IFlowable<T> source)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static Task IgnoreElementsTask<T>(this IFlowable<T> source)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        // ********************************************************************************
        // Blocking operators
        // ********************************************************************************

        public static bool BlockingFirst<T>(this IFlowable<T> source, out T result)
        {
            var s = new BlockingFirstSubscriber<T>();
            source.Subscribe(s);
            return s.BlockingGet(out result);
        }

        public static bool BlockingLast<T>(this IFlowable<T> source, out T result)
        {
            var s = new BlockingLastSubscriber<T>();
            source.Subscribe(s);
            return s.BlockingGet(out result);
        }

        public static IEnumerable<T> BlockingEnumerable<T>(this IFlowable<T> source)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        public static void BlockingSubscribe<T>(this IFlowable<T> source)
        {
            BlockingSubscribe(source, v => { }, e => { }, () => { });
        }

        public static void BlockingSubscribe<T>(this IFlowable<T> source, Action<T> onNext)
        {
            BlockingSubscribe(source, onNext, e => { }, () => { });
        }

        public static void BlockingSubscribe<T>(this IFlowable<T> source, Action<T> onNext, Action<T> onError)
        {
            BlockingSubscribe(source, onNext, onError, () => { });
        }

        public static void BlockingSubscribe<T>(this IFlowable<T> source, Action<T> onNext, Action<T> onError, Action onComplete)
        {
            // TODO implement
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
