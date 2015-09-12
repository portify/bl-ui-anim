function lerp(%t, %a, %b)
{
    return %a + (%b - %a) * %t;
}

function unlerp(%v, %a, %b)
{
    return (%v - %a) / (%b - %a);
}
