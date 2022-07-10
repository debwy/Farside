-> main

=== main ===
Welcome to Farside!
What would you like to know?
    + [About the village]
        -> chosen("the village, for there is none!")
    + [About you]
        -> chosen("myself, other than the fact that I'm quirky.")
    + [About the slimes]
        -> chosen("the slimes. Someone like me wouldn't be fighting slimes.")
        
=== chosen(answer) ===
I'm afraid I can't tell you about {answer}
-> END

//  -> enemies
/* 
=== enemies ===
I can tell you what I know about the enemies here, though! Ask away.
    + [Pink slimes]
        They're made up of elemental energy. As a result, they're super light and airy! I wonder if you can push them around...?
        -> enemies
        
    + [Red slimes]
        Be careful of these ones - they're immune to fire. The last time we threw bombs at 'em, nothing happened! Your best bet is to get up close and whack 'em silly.
        -> enemies

-> END */        
