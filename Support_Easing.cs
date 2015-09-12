// TODO:
// a p parameters for elastic

function easeInQuad(%t)
{
    return %t * %t;
}

function easeOutQuad(%t)
{
    return -1 * %t * (%t - 2);
}

function easeInOutQuad(%t)
{
    %t *= 2;
    if (%t < 1) return 0.5 * %t * %t;
    %t--;
    return -0.5 * (%t * (%t - 2) - 1);
}

function easeInCubic(%t)
{
    return %t * %t * %t;
}

function easeOutCubic(%t)
{
    %t--;
    return %t * %t * %t + 1;
}

function easeInOutCubic(%t)
{
    %t *= 2;
    if (%t < 1) return 0.5 * %t * %t * %t;
    %t -= 2;
    return 0.5 * (%t * %t * %t + 2);
}

function easeInQuart(%t)
{
    return %t * %t * %t * %t;
}

function easeOutQuart(%t)
{
    %t -= 1;
    return -1 * (%t * %t * %t * %t - 1);
}

function easeInOutQuart(%t)
{
    %t *= 2;

    if (%t < 1)
    {
        return 0.5 * %t * %t * %t * %t;
    }
    else
    {
        %t -= 2;
        return -0.5 * (%t * %t * %t * %t - 2);
    }
}

function easeInQuint(%t)
{
    return %t * %t * %t * %t * %t;
}

function easeOutQuint(%t)
{
    %t -= 1;
    return %t * %t * %t * %t * %t + 1;
}

function easeInOutQuint(%t)
{
    %t *= 2;

    if (%t < 1)
    {
        return 0.5 * %t * %t * %t * %t * %t;
    }
    else
    {
        %t -= 2;
        return 0.5 * (%t * %t * %t * %t * %t + 2);
    }
}

function easeInSine(%t)
{
    return - 1 * mCos(%t * ($pi / 2)) + 1;
}

function easeOutSine(%t)
{
    return mSin(%t * ($pi / 2));
}

function easeInOutSine(%t)
{
    return -0.5 * (mCos($pi * %t) - 1);
}

function easeInExpo(%t)
{
    if (%t == 0) return 0;
    return mPow(2, 10 * (%t - 1)) - 0.001;
}

function easeOutExpo(%t)
{
    if (%t == 1) return 1;
    return 1.001 * (-mPow(2, -10 * %t) + 1);
}

function easeInOutExpo(%t)
{
    if (%t == 0) return 0;
    if (%t == 1) return 1;

    %t *= 2;

    if (%t < 1)
    {
        return 0.5 * mPow(2, 10 * (%t - 1)) - 0.0005;
    }
    else
    {
        %t--;
        return 0.5 * 1.0005 * (-mPow(2, -10 * %t) + 2);
    }
}

function easeInCirc(%t)
{
    return -1 * (mSqrt(1 - mPow(%t, 2)) - 1);
}

function easeOutCirc(%t)
{
    %t -= 1;
    return mSqrt(1 - mPow(%t, 2));
}

function easeInOutCirc(%t)
{
    %t *= 2;

    if (%t < 1)
    {
        return -0.5 * (mSqrt(1 - %t * %t) - 1);
    }
    else
    {
        %t -= 2;
        return 0.5 * (mSqrt(1 - %t * %t) + 1);
    }
}

function easeInElastic(%t)
{
    if (%t == 0) return 0;
    if (%t == 1) return 1;

    %p = 0.3;
    %s = %p / 4;

    %t -= 1;
    return -(mPow(2, 10 * %t) * mSin((%t - %s) * (2 * $pi) / %p));
}

function easeOutElastic(%t)
{
    if (%t == 0) return 0;
    if (%t == 1) return 1;

    %p = 0.3;
    %s = %p / 4;

    return mPow(2, -10 * %t) * mSin((%t - %s) * (2 * $pi) / %p) + 1;
}

function easeInBack(%t, %s)
{
    if (%s $= "") %s = 1.70158;
    return %t * %t * ((%s + 1) * %t - %s);
}

function easeOutBack(%t, %s)
{
    if (%s $= "") %s = 1.70158;
    %t--;
    return %t * %t * ((%s + 1) * %t + %s) + 1;
}

function easeInOutBack(%t, %s)
{
    if (%s $= "") %s = 1.70158;
    %s *= 1.525;
    %t *= 2;

    if (%t < 1)
    {
        return 0.5 * (%t * %t * ((%s + 1) * %t - %s));
    }
    else
    {
        %t -= 2;
        return 0.5 * (%t * %t * ((%s + 1) * %t + %s) + 2);
    }
}

function easeInBounce(%t)
{
    return 1 - easeOutBounce(1 - %t);
}

function easeOutBounce(%t)
{
    if (%t < 1 / 2.75)
    {
        return 7.5625 * %t * %t;
    }
    else if (%t < 2 / 2.75)
    {
        %t -= 1.5 / 2.75;
        return 7.5625 * %t * %t + 0.75;
    }
    else if (%t < 2.5 / 2.75)
    {
        %t -= 2.25 / 2.75;
        return 7.5625 * %t * %t + 0.9375;
    }
    else
    {
        %t -= 2.625 / 2.75;
        return 7.5625 * %t * %t + 0.984375;
    }
}

function easeInOutBounce(%t)
{
    if (%t < 0.5)
        return easeInBounce(%t * 2) * 0.5;
    else
        return easeOutBounce(%t * 2 - 1) * 0.5 + 0.5;
}
