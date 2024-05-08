using FluentAssertions;
using TaskNotFound.Shared.Abstract.ErrorHandling.Exceptions;
using Xunit;

namespace TaskNotFound.Shared.Abstract.ErrorHandling.Results;

public class ResultTests
{
    public class WhenResultIsOk
    {
        private readonly TestValue _value;
        private readonly Result<TestValue, TestError> _result;

        public WhenResultIsOk()
        {
            _value = new();
            _result = Result.Ok<TestValue, TestError>(_value);
        }

        [Fact]
        public void IsOk_OnOk_ReturnsTrue()
        {
            _result.IsOk
                .Should()
                .BeTrue();
        }

        [Fact]
        public void IsError_OnOk_ReturnsFalse()
        {
            _result.IsError
                .Should()
                .BeFalse();
        }

        [Fact]
        public void Unwrap_OnOk_ReturnsValue()
        {
            _result.Unwrap()
                .Should()
                .Be(_value);
        }

        [Fact]
        public void UnwrapError_OnOk_ThrowsUnwrapException()
        {
            _result.Invoking(res => res.UnwrapError())
                .Should()
                .Throw<UnwrapException>();
        }

        [Fact]
        public void UnwrapOr_OnOk_ReturnsValue()
        {
            var @default = new TestValue();

            _result.UnwrapOr(@default)
                .Should()
                .Be(_value);
        }

        [Fact]
        public void UnwrapOrElse_OnOk_ReturnsValue()
        {
            var @default = new TestValue();

            _result.UnwrapOrElse(_ => @default)
                .Should()
                .Be(_value);
        }

        [Fact]
        public void UnwrapOrElseThrow_OnOk_ReturnsValue()
        {
            var exception = new Exception();

            _result.UnwrapOrElseThrow(_ => exception)
                .Should()
                .Be(_value);
        }

        [Fact]
        public void Map_OnOk_MapsValue()
        {
            var mappedValue = new TestValue();

            var mapped = _result.Map(_ => mappedValue);

            mapped.Unwrap()
                .Should()
                .Be(mappedValue);
        }

        [Fact]
        public void MapError_OnOk_DoingNothing()
        {
            var mappedError = new TestError();

            var mapped = _result.MapError(_ => mappedError);

            mapped.Unwrap()
                .Should()
                .Be(_value);
        }

        [Fact]
        public void Then_OnOk_MapsResult()
        {
            var nextValue = new TestValue();
            var next = Result.Ok<TestValue, TestError>(nextValue);

            var mapped = _result.Then(_ => next);

            mapped.Should()
                .Be(next);
        }

        [Fact]
        public void Or_OnOk_DoingNothing()
        {
            var alternativeValue = new TestValue();
            var alternative = Result.Ok<TestValue, TestError>(alternativeValue);

            var mapped = _result.Or(_ => alternative);

            mapped.Should()
                .Be(_result);
        }

        [Fact]
        public void WhenOk_OnOk_DoingAction()
        {
            const int expected = 1;
            var current = 0;

            _result.WhenOk(_ => current += 1);

            current.Should()
                .Be(expected);
        }

        [Fact]
        public void WhenError_OnOk_DoingNothing()
        {
            const int expected = 0;
            var current = 0;

            _result.WhenError(_ => current += 1);

            current.Should()
                .Be(expected);
        }

        [Fact]
        public async void MapAsync_OnOk_MapsValue()
        {
            var mappedValue = new TestValue();

            var mapped = await _result.MapAsync(_ => Task.FromResult(mappedValue));

            mapped.Unwrap()
                .Should()
                .Be(mappedValue);
        }

        [Fact]
        public async void MapErrorAsync_OnOk_DoingNothing()
        {
            var mappedError = new TestError();

            var mapped = await _result.MapErrorAsync(_ => Task.FromResult(mappedError));

            mapped.Unwrap()
                .Should()
                .Be(_value);
        }

        [Fact]
        public async void ThenAsync_OnOk_MapsResult()
        {
            var nextValue = new TestValue();
            var next = Result.Ok<TestValue, TestError>(nextValue);

            var mapped = await _result.ThenAsync(_ => Task.FromResult(next));

            mapped.Should()
                .Be(next);
        }

        [Fact]
        public async void OrAsync_OnOk_DoingNothing()
        {
            var alternativeValue = new TestValue();
            var alternative = Result.Ok<TestValue, TestError>(alternativeValue);

            var mapped = await _result.OrAsync(_ => Task.FromResult(alternative));

            mapped.Should()
                .Be(_result);
        }

        [Fact]
        public async void WhenOkAsync_OnOk_DoingAction()
        {
            const int expected = 1;
            var current = 0;

            await _result.WhenOkAsync(_ => Task.FromResult(current += 1));

            current.Should()
                .Be(expected);
        }

        [Fact]
        public async void WhenErrorAsync_OnOk_DoingNothing()
        {
            const int expected = 0;
            var current = 0;

            await _result.WhenErrorAsync(_ => Task.FromResult(current += 1));

            current.Should()
                .Be(expected);
        }
    }

    public class WhenResultIsError
    {
        private readonly TestError _error;
        private readonly Result<TestValue, TestError> _result;

        public WhenResultIsError()
        {
            _error = new();
            _result = Result.Error<TestValue, TestError>(_error);
        }

        [Fact]
        public void IsOk_OnError_ReturnsFalse()
        {
            _result.IsOk
                .Should()
                .BeFalse();
        }

        [Fact]
        public void IsError_OnError_ReturnsTrue()
        {
            _result.IsError
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Unwrap_OnError_ThrowsUnwrapException()
        {
            _result.Invoking(res => res.Unwrap())
                .Should()
                .Throw<UnwrapException>();
        }

        [Fact]
        public void UnwrapError_OnError_ReturnsError()
        {
            _result.UnwrapError()
                .Should()
                .Be(_error);
        }

        [Fact]
        public void UnwrapOr_OnError_ReturnsDefault()
        {
            var @default = new TestValue();

            _result.UnwrapOr(@default)
                .Should()
                .Be(@default);
        }

        [Fact]
        public void UnwrapOrElse_OnError_ReturnsDefault()
        {
            var @default = new TestValue();

            _result.UnwrapOrElse(_ => @default)
                .Should()
                .Be(@default);
        }

        [Fact]
        public void UnwrapOrElseThrow_OnError_ThrowsException()
        {
            var exception = new Exception();

            _result.Invoking(res => res.UnwrapOrElseThrow(_ => exception))
                .Should()
                .Throw<Exception>();
        }

        [Fact]
        public void Map_OnError_DoingNothing()
        {
            var mappedValue = new TestValue();

            var mapped = _result.Map(_ => mappedValue);

            mapped.UnwrapError()
                .Should()
                .Be(_error);
        }

        [Fact]
        public void MapError_OnError_MapsError()
        {
            var mappedError = new TestError();

            var mapped = _result.MapError(_ => mappedError);

            mapped.UnwrapError()
                .Should()
                .Be(mappedError);
        }

        [Fact]
        public void Then_OnError_DoingNothing()
        {
            var nextValue = new TestValue();
            var next = Result.Ok<TestValue, TestError>(nextValue);

            var mapped = _result.Then(_ => next);

            mapped.Should()
                .Be(_result);
        }

        [Fact]
        public void Or_OnError_MapsResult()
        {
            var alternativeValue = new TestValue();
            var alternative = Result.Ok<TestValue, TestError>(alternativeValue);

            var mapped = _result.Or(_ => alternative);

            mapped.Should()
                .Be(alternative);
        }

        [Fact]
        public void WhenOk_OnError_DoingNothing()
        {
            const int expected = 0;
            var current = 0;

            _result.WhenOk(_ => current += 1);

            current.Should()
                .Be(expected);
        }

        [Fact]
        public void WhenError_OnError_DoingAction()
        {
            const int expected = 1;
            var current = 0;

            _result.WhenError(_ => current += 1);

            current.Should()
                .Be(expected);
        }

        [Fact]
        public async void MapAsync_OnError_DoingNothing()
        {
            var mappedValue = new TestValue();

            var mapped = await _result.MapAsync(_ => Task.FromResult(mappedValue));

            mapped.UnwrapError()
                .Should()
                .Be(_error);
        }

        [Fact]
        public async void MapErrorAsync_OnError_MapsError()
        {
            var mappedError = new TestError();

            var mapped = await _result.MapErrorAsync(_ => Task.FromResult(mappedError));

            mapped.UnwrapError()
                .Should()
                .Be(mappedError);
        }

        [Fact]
        public async void ThenAsync_OnError_DoingNothing()
        {
            var nextValue = new TestValue();
            var next = Result.Ok<TestValue, TestError>(nextValue);

            var mapped = await _result.ThenAsync(_ => Task.FromResult(next));

            mapped.Should()
                .Be(_result);
        }

        [Fact]
        public async void OrAsync_OnError_MapsResult()
        {
            var alternativeValue = new TestValue();
            var alternative = Result.Ok<TestValue, TestError>(alternativeValue);

            var mapped = await _result.OrAsync(_ => Task.FromResult(alternative));

            mapped.Should()
                .Be(alternative);
        }

        [Fact]
        public async void WhenOkAsync_OnError_DoingNothing()
        {
            const int expected = 0;
            var current = 0;

            await _result.WhenOkAsync(_ => Task.FromResult(current += 1));

            current.Should()
                .Be(expected);
        }

        [Fact]
        public async void WhenErrorAsync_OnError_DoingAction()
        {
            const int expected = 1;
            var current = 0;

            await _result.WhenErrorAsync(_ => Task.FromResult(current += 1));

            current.Should()
                .Be(expected);
        }
    }
}
