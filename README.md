# LuauExpressionify

Rewrites Luau scripts to make them *mostly* use expressions, but mostly obfuscates the file in funny ways.

# Usage

```
expressionify.exe input.lua
```


### Example Input

```lua
print('hello world')
local v = print('goodbye world')
if 'from expressionify' == 'from expressionify' then 
    print'from expressionify!'
elseif 'this does not pass' then 
    print'not me :('
end 
if 'from expressionify' == 'from expressionify' then 
    print'from expressionify!'
elseif 'this does not pass' then 
    print'not me :('
end 
if 'from expressionify' == 'from expressionify' then 
    print'from expressionify!'
elseif 'this does not pass' then 
    print'not me :('
end 

do print'do block!' end

print(("Wrapped Hello!"))
print(5 + 10)

if a == nil then 
    print'a was obviously nil'
else 
    print(0/0)
end 

local function foo()
    print'from foo!'
end 

foo()

local a = 50
a = a + 50
a += 25
print(a)

local t = {}
print(t[1])

local t2 = {
    ident = true,
    [5] = false,
    "hi", "bye"
}

print(t2.ident, t2[5], t2[1], t2[2])

local b = 1
while b ~= 5 do 
    b = b + 1
end 
print(b)

for i = 1, 2 do 
    print(i)
end 

for i = 100, 1000, 100 do 
    print(i)
end 

for i,v in {1,2,3,4,5} do 
    print(i,v)
end 

for i,v in pairs({5,4,3,2,1}) do 
    print(i,v)
end 

for i,v in pairs(math) do 
    print(i)
end 
```

### Example Output

```lua
(function()
    return if "'if if if ' - SnowyShiro" then (function()
        return print
    end)("This is depressing") else "https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif"
end)()('hello world');
local v = if "https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif" then (function()
    return print
end)("This file was rewritten by LuauExpressionify :)")('goodbye world') else "This is depressing"
local ToHellWithYourOvertime = if (if true then 'from expressionify' else false) == (if true then 'from expressionify' else false) then (function()
    (function()
        return if "'if if if ' - SnowyShiro" then (function()
            return print
        end)("'if if if ' - SnowyShiro") else "(function() while true do end end)()"
    end)() 'from expressionify!';
end)() elseif 'this does not pass' then (function()
    (function()
        return if "https://www.youtube.com/watch?v=nmxueMo9qMU" then (function()
            return print
        end)("I use arch btw") else "Painfuck"
    end)() 'not me :(';
end)() else "'if if if ' - SnowyShiro"
local ToHellWithYourOvertime = if (if true then 'from expressionify' else false) == (if true then 'from expressionify' else false) then (function()
    (function()
        return if "Deobfuscated: https://media.discordapp.net/attachments/532907112515502084/943000282181283890/scripting.gif" then (function()
            return print
        end)("This is depressing") else "'painfuck, i love that' - SnowyShiro"
    end)() 'from expressionify!';
end)() elseif 'this does not pass' then (function()
    (function()
        return if "I am not responsible for anything" then (function()
            return print
        end)("https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif") else "https://www.youtube.com/watch?v=nmxueMo9qMU"
    end)() 'not me :(';
end)() else "I use arch btw"
if if (if true then 'from expressionify' else false) == (if true then 'from expressionify' else false) then "I am not responsible for anything" else false then
    (function()
        return if "(function() while true do end end)()" then (function()
            return print
        end)("'if if if ' - SnowyShiro") else "This file was rewritten by LuauExpressionify :)"
    end)() 'from expressionify!';
elseif if if 'this does not pass' then "You shall not pass!" else false then "https://www.youtube.com/watch?v=nmxueMo9qMU" else false then
    (function()
        return if "https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif" then (function()
            return print
        end)("This file was rewritten by LuauExpressionify :)") else "'if if if ' - SnowyShiro"
    end)() 'not me :(';
end
(function()
    print 'do block!'
end)();
(function()
    return if "Painfuck" then (function()
        return print
    end)("This file was rewritten by LuauExpressionify :)") else "You shall not pass!"
end)()(("Wrapped Hello!" :: HiFromAcedia));
(function()
    return if "You shall not pass!" then (function()
        return print
    end)("You shall not pass!") else "This is depressing"
end)()((if true then (-31 + #"'painfuck, i love that' - SnowyShiro") else false) + (if true then (-8 + #"This is depressing") else false));
if if if if (if true then a else false) == (if true then nil else false) then "https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif" else false then "This is depressing" else false then "You shall not pass!" else false then
    (function()
        return if "Painfuck" then (function()
            return print
        end)("This file was rewritten by LuauExpressionify :)") else "'if if if ' - SnowyShiro"
    end)() 'a was obviously nil';
else
    (function()
        return if "https://i.imgur.com/x9O8Sri.png" then (function()
            return print
        end)("'if if if ' - SnowyShiro") else "https://www.youtube.com/watch?v=nmxueMo9qMU"
    end)()((if true then (-31 + #"https://i.imgur.com/x9O8Sri.png") else false) / (if true then (-43 + #"https://www.youtube.com/watch?v=nmxueMo9qMU") else false));
end
type Oh<Jesus> = typeof(function()
    (function()
        return if "'painfuck, i love that' - SnowyShiro" then (function()
            return print
        end)("Deobfuscated: https://media.discordapp.net/attachments/532907112515502084/943000282181283890/scripting.gif") else "This is depressing"
    end)() 'from expressionify!';
end)
local foo = if true then function()
    (function()
        return if "https://www.youtube.com/watch?v=nmxueMo9qMU" then (function()
            return print
        end)("I am not responsible for anything") else "This file was rewritten by LuauExpressionify :)"
    end)() 'from foo!';
end else false
(function()
    return if "(function() while true do end end)()" then (function()
        return foo
    end)("You shall not pass!") else "This file was rewritten by LuauExpressionify :)"
end)()();
local a = (17 + #"I am not responsible for anything")
type Oh<Jesus> = typeof(function()
    (function()
        return if "https://www.youtube.com/watch?v=tVjk_aFdfF8" then (function()
            return print
        end)("https://www.youtube.com/watch?v=tVjk_aFdfF8") else "Painfuck"
    end)() 'not me :(';
end)
a = (if true then a else false) + (if true then (17 + #"I am not responsible for anything") else false)
a = a + if true then 25 else false
(function()
    return if "https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif" then (function()
        return print
    end)("Painfuck") else "This is depressing"
end)()(a);
local t = {}
(function()
    return if "https://www.youtube.com/watch?v=nmxueMo9qMU" then (function()
        return print
    end)("https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif") else "Painfuck"
end)()((if true then t else false)[1]);
local t2 = { [if true then "ident" else false] = true, [if true then 5 else false] = false, [if true then 1 else false] = "hi", [if true then 2 else false] = "bye" }
(function()
    return if "You shall not pass!" then (function()
        return print
    end)("'if if if ' - SnowyShiro") else "https://i.imgur.com/x9O8Sri.png"
end)()(t2.ident, (if true then t2 else false)[5], (if true then t2 else false)[1], (if true then t2 else false)[2]);
local b = (-7 + #"Painfuck")
while if (if true then b else false) ~= (if true then (-13 + #"This is depressing") else false) then "https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif" else false do
    b = b + 1
end
type Oh<Jesus> = typeof(function()
    (function()
        return if "https://www.youtube.com/watch?v=tVjk_aFdfF8" then (function()
            return print
        end)("Painfuck") else "Racel cant reverse"
    end)() 'not me :(';
end)
(function()
    return if "I am not responsible for anything" then (function()
        return print
    end)("This file was rewritten by LuauExpressionify :)") else "Painfuck"
end)()(b);
type Oh<Jesus> = typeof(function()
    (function()
        return if "I am not responsible for anything" then (function()
            return print
        end)("Deobfuscated: https://media.discordapp.net/attachments/532907112515502084/943000282181283890/scripting.gif") else "https://www.youtube.com/watch?v=nmxueMo9qMU"
    end)() 'do block!';
end)
for i = if true then 1 else true, if true then 2 else true do
    (function()
        return if "https://www.youtube.com/watch?v=tVjk_aFdfF8" then (function()
            return print
        end)("https://www.youtube.com/watch?v=tVjk_aFdfF8") else "Racel cant reverse"
    end)()(i);
end
for i = if true then 100 else true, if true then 1000 else true, if true then 100 else true do
    (function()
        return if "Deobfuscated: https://media.discordapp.net/attachments/532907112515502084/943000282181283890/scripting.gif" then (function()
            return print
        end)("This file was rewritten by LuauExpressionify :)") else "'if if if ' - SnowyShiro"
    end)()(i);
end
for i, v in { 1, 2, 3, 4, 5 } do
    (function()
        return if "https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif" then (function()
            return print
        end)("https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif") else "'painfuck, i love that' - SnowyShiro"
    end)()(i, v);
end
for i, v in pairs({ 5, 4, 3, 2, 1 }) do
    (function()
        return if "'if if if ' - SnowyShiro" then (function()
            return print
        end)("https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif") else "(function() while true do end end)()"
    end)()(i, v);
end
type Oh<Jesus> = typeof(function()
    (function()
        return if "https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif" then (function()
            return print
        end)("'if if if ' - SnowyShiro") else "https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif"
    end)()(i, v);
end)
for i, v in pairs(math) do
    (function()
        return if "'painfuck, i love that' - SnowyShiro" then (function()
            return print
        end)("https://www.youtube.com/watch?v=nmxueMo9qMU") else "https://www.youtube.com/watch?v=tVjk_aFdfF8"
    end)()(i);
end
```
