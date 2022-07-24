INCLUDE globals.ink

{ groundskeeper:
- 0: -> main
- 1: -> talked
- 2: -> completed
}

=== main ===
~ groundskeeper = 1
Uh...you talking to me? 
 * [Who are you?]
I'm...one of the groundskeepers here. Yeah. Of course, I'm doing my job...
It's just, y'know, I need some time to prepare myself...and check on other stuff...
I'm not scared...I definitely am going to beat that metal hunk up...eventually... 

-> enemies

=== enemies ===
 * [Tell me about it]
    That hunk of junk is pretty terrifying...
    It shoots you relentlessly...if only I could use ranged attacks from afar...then I wouldn't have to cower in a corner...
    -> enemies
 * [OK, dude]
     I didn't sign up for this...
     -> DONE
 * ->
-> END

=== talked ===

{ golemDeaths_global:
- 0: I-if that thing could disappear...that would be great...

- else:
O-oh...did you actually manage to defeat it?
I'm s-so impressed...and I didn't even ask you to defeat it for me...
T-thank you!
~ groundskeeper = 2
~ helped_global += 1
}
-> END

=== completed ===
Y-you're so strong...I'll aspire to be just like you...

-> END





