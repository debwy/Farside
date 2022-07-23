INCLUDE globals.ink

{graymouse == 1: -> completed}
{jumpinghint == 0: -> main | -> final}

=== main ===
~ jumpinghint = 1

<size=45>Oh, a human!</size>
I haven't seen your kind in a long time. The last time I saw one, it was...<size=35>nevermind.</size>
Oops, I meant to say he, not it. Humans don't like being called it, right?
Anyway, check out this neat little verse! 
<size=50><color=yellow>The golden steps tracing the canopy...</color></size>
<size=50><color=yellow>Awaits those who do not falter...</color></size>
<size=50><color=yellow>Be brave and venture up high, young hero.</color></size>
Cool, huh? I wonder what it means?

-> END

=== final ===
Heya, human! Wanna hear the verse again?
    * [Yes]
<size=50><color=yellow>The golden steps tracing the canopy...</color></size>
<size=50><color=yellow>Awaits those who do not falter...</color></size>
<size=50><color=yellow>Be brave and venture up high, young hero.</color></size>
Cool, huh? I wonder what it means?
-> END
    
    * [No]
Suit yourself, human!
-> END

=== completed ===
Wowie! I somehow get the feeling that you don't need to hear the verse anymore. 
Maybe some praise is in order!

-> END