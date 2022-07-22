INCLUDE globals.ink

{ graymouse:
- 0: -> main
- 1: -> already_spoken 
- 2: -> cancelled
}

=== main ===
<size=45>SQUEAK!</size> w-w-who are you? w-why are you here?
i..i climbed and jumped all the way here to finally find some peace...
and what do i see?! A HUMAN! a human who jumped endlessly to meet me!
are you crazy? huh?!
u-um...just leave! please! go!

 + [I need a favour...]
    ~ graymouse = 1
    -> favour
 + [Sorry to disturb you...]
    ~ graymouse = 2
-> END

=== favour ===
~ helped_global += 1 //assigns chosen purpose into globals.ink
a-a favour? just say it! say it already!
o-ok, ok, i got it, ill give you ten recommendations or anything you need, just go!!!
<size=35>except i dont think they will count it as ten...but you know...ill try...</size>
-> END

=== cancelled ===
why are u talking to me again?! huh? 
 + [I need a favour...]
    ~ graymouse = 1
    -> favour

=== already_spoken ===
what else do u want, human?! just leave me alone!

-> END


