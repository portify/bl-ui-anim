function cancelAnimateUI(%data)
{
    $AnimatedUI::Cancelled[%data] = true;
}

function GUIControl::animate(%this, %duration, %props, %interp)
{
    if (!isObject(%props))
    {
        warn("::animate - missing props");
        return false;
    }

    for (%i = 0; true; %i++)
    {
        %tag = %props.getTaggedField(%i);

        if (%tag $= "")
            break;

        %field = getField(%tag, 0);

        switch$ (%field)
        {
            case "position": %oldValue[%oldMax++] = %this.getPosition();
            case "extent": %oldValue[%oldMax++] = %this.getExtent();
            case "color": %oldValue[%oldMax++] = %this.getColor();

            default:
                warn("::animate - unknown property " @ %field);
                continue;
        }

        %oldField[%oldMax] = %field;
    }

    for (%i = 1; %i <= %oldMax; %i++)
    {
        %props._old[%oldField[%i]] = %oldValue[%i];
    }

    if ($AnimatedUI::NextData $= "")
        $AnimatedUI::NextData = 0;

    %data = $AnimatedUI::NextData;
    $AnimatedUI::NextData = ($AnimatedUI::NextData + 1) | 0;

    $AnimatedUI::Control[%data] = %this;
    $AnimatedUI::Start[%data] = $Sim::Time;
    $AnimatedUI::Duration[%data] = %duration;
    $AnimatedUI::Cancelled[%data] = false;
    $AnimatedUI::Props[%data] = %props;
    $AnimatedUI::Interp[%data] = %interp;

    addFrameTick("doAnimateUI", %data);
    return %data;
}

function doAnimateUI(%data)
{
    %control = $AnimatedUI::Control[%data];
    %props = $AnimatedUI::Props[%data];

    if (!isObject(%props))
        return true;

    if (!isObject(%control) || $AnimatedUI::Cancelled[%data])
    {
        %props.delete();
        return true;
    }

    %start = $AnimatedUI::Start[%data];
    %duration = $AnimatedUI::Duration[%data];

    %t = mClampF(($Sim::Time - %start) / %duration, 0, 1);

    if (isFunction($AnimatedUI::Interp[%data]))
        %t = call($AnimatedUI::Interp[%data], %t);

    %shouldResize = false;
    %x = getWord(%control.position, 0);
    %y = getWord(%control.position, 1);
    %w = getWord(%control.extent, 0);
    %h = getWord(%control.extent, 1);

    for (%i = 0; true; %i++)
    {
        %tag = %props.getTaggedField(%i);

        if (%tag $= "")
            break;

        %field = getField(%tag, 0);

        if (getSubStr(%field, 0, 4) $= "_old")
            continue;

        %a = %props._old[%field];
        %b = getFields(%tag, 1, getFieldCount(%tag));

        switch$ (%field)
        {
            case "position":
                %shouldResize = true;
                %x = lerp(%t, getWord(%a, 0), getWord(%b, 0));
                %y = lerp(%t, getWord(%a, 1), getWord(%b, 1));

            case "extent":
                %shouldResize = true;
                %w = lerp(%t, getWord(%a, 0), getWord(%b, 0));
                %h = lerp(%t, getWord(%a, 1), getWord(%b, 1));

            case "color":
                %r = lerp(%t, getWord(%a, 0), getWord(%b, 0));
                %g = lerp(%t, getWord(%a, 1), getWord(%b, 1));
                %b = lerp(%t, getWord(%a, 2), getWord(%b, 2));
                %a = lerp(%t, getWord(%a, 3), getWord(%b, 3));
                %control.setColor(%r SPC %g SPC %b SPC %a);
        }
    }

    if (%shouldResize)
        %control.resize(%x, %y, %w, %h);

    if ($Sim::Time >= %start + %duration)
    {
        %props.delete();
        return true;
    }

    return false;
}
