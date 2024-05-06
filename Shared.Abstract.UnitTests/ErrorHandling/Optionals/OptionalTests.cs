using FluentAssertions;
using TaskNotFound.Shared.Abstract.ErrorHandling.Exceptions;
using Xunit;

namespace TaskNotFound.Shared.Abstract.ErrorHandling.Optionals;

public class OptionalTests
{
    public class WhenOptionalIsPresent
    {
        private readonly TestValue _value;
        private readonly Optional<TestValue> _optional;

        public WhenOptionalIsPresent()
        {
            _value = new();
            _optional = Optional.Of(_value);
        }

        [Fact]
        public void IsPresent_OnPresent_ReturnsTrue()
        {
            _optional.IsPresent
                .Should()
                .BeTrue();
        }

        [Fact]
        public void IsEmpty_OnPresent_ReturnsFalse()
        {
            _optional.IsEmpty
                .Should()
                .BeFalse();
        }

        [Fact]
        public void Unwrap_OnPresent_ReturnsValue()
        {
            _optional.Unwrap()
                .Should()
                .Be(_value);
        }

        [Fact]
        public void UnwrapOr_OnPresent_ReturnsValue()
        {
            var @default = new TestValue();

            _optional.UnwrapOr(@default)
                .Should()
                .Be(_value);
        }

        [Fact]
        public void UnwrapOrElse_OnPresent_ReturnsValue()
        {
            var @default = new TestValue();

            _optional.UnwrapOrElse(() => @default)
                .Should()
                .Be(_value);
        }

        [Fact]
        public void UnwrapOrElseThrow_OnPresent_ReturnsValue()
        {
            var exception = new Exception();

            _optional.UnwrapOrElseThrow(() => exception)
                .Should()
                .Be(_value);
        }

        [Fact]
        public void Map_OnPresent_MapsValue()
        {
            var mappedValue = new TestValue();

            var mapped = _optional.Map(_ => mappedValue);

            mapped.Unwrap()
                .Should()
                .Be(mappedValue);
        }

        [Fact]
        public void Then_OnPresent_MapsOptional()
        {
            var nextValue = new TestValue();
            var next = Optional.Of(nextValue);

            var mapped = _optional.Then(_ => next);

            mapped.Should()
                .Be(next);
        }

        [Fact]
        public void Or_OnPresent_DoingNothing()
        {
            var alternativeValue = new TestValue();
            var alternative = Optional.Of(alternativeValue);

            var mapped = _optional.Or(() => alternative);

            mapped.Should()
                .Be(_optional);
        }

        [Fact]
        public void WhenPresent_OnPresent_DoingAction()
        {
            const int expected = 1;
            var current = 0;

            _optional.WhenPresent(_ => current += 1);

            current.Should()
                .Be(expected);
        }

        [Fact]
        public void WhenEmpty_OnPresent_DoingNothing()
        {
            const int expected = 0;
            var current = 0;

            _optional.WhenEmpty(() => current += 1);

            current.Should()
                .Be(expected);
        }

        [Fact]
        public async void MapAsync_OnPresent_MapsValue()
        {
            var mappedValue = new TestValue();

            var mapped = await _optional.MapAsync(_ => Task.FromResult(mappedValue));

            mapped.Unwrap()
                .Should()
                .Be(mappedValue);
        }

        [Fact]
        public async void ThenAsync_OnPresent_MapsValue()
        {
            var nextValue = new TestValue();
            var next = Optional.Of(nextValue);

            var mapped = await _optional.ThenAsync(_ => Task.FromResult(next));

            mapped.Should()
                .Be(next);
        }

        [Fact]
        public async void OrAsync_OnPresent_DoingNothing()
        {
            var alternativeValue = new TestValue();
            var alternative = Optional.Of(alternativeValue);

            var mapped = await _optional.OrAsync(() => Task.FromResult(alternative));

            mapped.Should()
                .Be(_optional);
        }

        [Fact]
        public async void WhenPresentAsync_OnPresent_DoingAction()
        {
            const int expected = 1;
            var current = 0;

            await _optional.WhenPresentAsync(_ => Task.FromResult(current += 1));

            current.Should()
                .Be(expected);
        }

        [Fact]
        public async void WhenEmptyAsync_OnPresent_DoingNothing()
        {
            const int expected = 0;
            var current = 0;

            await _optional.WhenEmptyAsync(() => Task.FromResult(current += 1));

            current.Should()
                .Be(expected);
        }
    }

    public class WhenOptionalIsEmpty
    {
        private readonly Optional<TestValue> _optional;

        public WhenOptionalIsEmpty()
        {
            _optional = Optional.Empty<TestValue>();
        }

        [Fact]
        public void IsPresent_OnEmpty_ReturnsFalse()
        {
            _optional.IsPresent
                .Should()
                .BeFalse();
        }

        [Fact]
        public void IsEmpty_OnEmpty_ReturnsTrue()
        {
            _optional.IsEmpty
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Unwrap_OnEmpty_ThrowsUnwrapException()
        {
            _optional.Invoking(opt => opt.Unwrap())
                .Should()
                .Throw<UnwrapException>();
        }

        [Fact]
        public void UnwrapOr_OnEmpty_ReturnsDefault()
        {
            var @default = new TestValue();

            _optional.UnwrapOr(@default)
                .Should()
                .Be(@default);
        }

        [Fact]
        public void UnwrapOrElse_OnEmpty_ReturnsDefault()
        {
            var @default = new TestValue();

            _optional.UnwrapOrElse(() => @default)
                .Should()
                .Be(@default);
        }

        [Fact]
        public void UnwrapOrElseThrow_OnEmpty_ThrowsException()
        {
            var exception = new Exception();

            _optional.Invoking(opt => opt.UnwrapOrElseThrow(() => exception))
                .Should()
                .Throw<Exception>();
        }

        [Fact]
        public void Map_OnEmpty_DoingNothing()
        {
            var mappedValue = new TestValue();

            var mapped = _optional.Map(_ => mappedValue);

            mapped.Should()
                .Be(_optional);
        }

        [Fact]
        public void Then_OnEmpty_DoingNothing()
        {
            var nextValue = new TestValue();
            var next = Optional.Of(nextValue);

            var mapped = _optional.Then(_ => next);

            mapped.Should()
                .Be(_optional);
        }

        [Fact]
        public void Or_OnEmpty_MapsOptional()
        {
            var alternativeValue = new TestValue();
            var alternative = Optional.Of(alternativeValue);

            var mapped = _optional.Or(() => alternative);

            mapped.Should()
                .Be(alternative);
        }

        [Fact]
        public void WhenPresent_OnEmpty_DoingNothing()
        {
            const int expected = 0;
            var current = 0;

            _optional.WhenPresent(_ => current += 1);

            current.Should()
                .Be(expected);
        }

        [Fact]
        public void WhenEmpty_OnEmpty_DoingAction()
        {
            const int expected = 1;
            var current = 0;

            _optional.WhenEmpty(() => current += 1);

            current.Should()
                .Be(expected);
        }

        [Fact]
        public async void MapAsync_OnEmpty_DoingNothing()
        {
            var mappedValue = new TestValue();

            var mapped = await _optional.MapAsync(_ => Task.FromResult(mappedValue));

            mapped.Should()
                .Be(_optional);
        }

        [Fact]
        public async void ThenAsync_OnEmpty_DoingNothing()
        {
            var nextValue = new TestValue();
            var next = Optional.Of(nextValue);

            var mapped = await _optional.ThenAsync(_ => Task.FromResult(next));

            mapped.Should()
                .Be(_optional);
        }

        [Fact]
        public async void OrAsync_OnEmpty_MapsOptional()
        {
            var alternativeValue = new TestValue();
            var alternative = Optional.Of(alternativeValue);

            var mapped = await _optional.OrAsync(() => Task.FromResult(alternative));

            mapped.Should()
                .Be(alternative);
        }

        [Fact]
        public async void WhenPresentAsync_OnEmpty_DoingNothing()
        {
            const int expected = 0;
            var current = 0;

            await _optional.WhenPresentAsync(_ => Task.FromResult(current += 1));

            current.Should()
                .Be(expected);
        }

        [Fact]
        public async void WhenEmptyAsync_OnEmpty_DoingAction()
        {
            const int expected = 1;
            var current = 0;

            await _optional.WhenEmptyAsync(() => Task.FromResult(current += 1));

            current.Should()
                .Be(expected);
        }
    }
}
