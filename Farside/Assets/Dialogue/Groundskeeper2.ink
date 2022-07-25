INCLUDE globals.ink

{ groundskeeper:
- 0: -> main
- 1: -> talked
- 2: -> completed
}

=== main ===
~ groundskeeper = 1
U-uh...you talking to me? 
 * [Who are you?]
I'm...one of the groundskeepers here. Yeah. Of c-course, I'm doing my job...
It's just, y'know, I need some time to prepare myself...and check on my laundry...
I'm not scared...I definitely am going to <color=yellow>beat that metal hunk to the right</color>...e-eventually... 

-> enemies

=== enemies ===
 * [Tell me about it]
    That hunk of junk is p-pretty terrifying...
    It shoots you relentlessly...if only I could use ranged attacks from afar...then I wouldn't have to cower in a corner...
    Maybe parrying would work too...but I don't know...
    -> enemies
 * [OK, dude]
     I didn't sign up for this...
     -> DONE
 * ->
-> END

=== talked ===

{ specificgolem:
- 0: <color=yellow>I-if that thing could disappear</color>...that would be great...

- else:
O-oh...did you actually manage to defeat it?
I'm s-so impressed...and I didn't even ask you to defeat it for me...
T-thank you! I'll definitely give the town head my recommendation!
~ groundskeeper = 2
~ helped_global += 1
}
-> END

=== completed ===
Y-you're so strong...I'll aspire to be just like you...
Yes, I've given the town head my approval...you can be sure of that...

-> END





