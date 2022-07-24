INCLUDE globals.ink

{ dog:
- 0: -> main
- 1: -> talked 
- 2: -> finish
}

=== main ===
Hiya, human. Yes, I'm a groundskeeper as well. 
I'm here to tell you about traps - but then again, do I even need to at this point?
I think you've figured it out already, since you made it all the way here! Big oopsie on my part.
I was going to give you some pointers at the entrance of the forest, but...I lost my precious item, so I got distracted searching for it. Sorry!
 * [I'll help you look]
Oh, you'll look for it too? That's really kind of you!
The item I lost is my <color=yellow>invisible hat</color>. Yes, you heard right. My precious hat's invisible, that's why I'm having a tough time finding it!
The last thing I remember is <color=yellow>putting my invisible hat into a chest</color>. But here's the catch - there are many chests all over the place!
You know what? Just open them alll up and hand me the contents!
Thanks, I'm counting on you! Remember, <color=yellow>open any chests you see and come find me!</color>
~ dog = 1
-> END

=== talked ===
Hiya, human!

{ dog == 2: -> finish }

{ chestOpenCount_global:
- 0: Well, you haven't found any chests yet, huh... <color=yellow>Open them and come find me!</color>
- 1: Oh! You opened a chest! Wow, I never thought you'd find one before I did!
Hm...oh? This is...?!
Nope, that invisible fedora doesn't look familiar! I'm afraid it isn't my invisible hat. Sorry to disappoint :(
Come back after you <color=yellow>open more chests</color>!
- 2: Wow, you're back with two chests opened! Let's see, let's see...
This...could it be?! Let me look at it, give it here!!
Oopsie, that invisible cap isn't mine! Looks like it's a bust this time round.
Well, no matter. I think you're close to finding the real one! Just <color=yellow>open more chests and let me take a look again</color>!
- 3: OOOH, three chests opened! This seems promising, don't you think? Let me take a look!
This material...this weight...hmm...
The size of the hat...the intricate knit pattern...the number of frayed ends... the burnt smell from dropping it into traps...
............
<size=50>This is my hat! My beanie!</size>
Thank you so much! I got my invisible beanie back! 
If you hadn't opened all those chests, I wouldn't be able to reunite with my little beanie!
Yes, I'll get you my recommendation immediately! Of course I will!
Well, I think this is a job well done! You're the bestest human around!
~ helped_global += 1
~ dog = 2
}
-> END

=== finish ===
Thank you! You're seriously the best!
I'll never let my beanie out of my sight again, mark my words!

-> END




