INCLUDE globals.ink

{ purpose_global == "": -> main | -> already_chosen }

=== main ===
Outpost is clear. No enemies sighted.

State your purpose, human.
 + [I'm a visitor]
    -> chosen("visitor")
 + [I'm here to help]
    -> chosen("helper")
 + [I'm a spy]
    -> chosen("spy")
    

=== chosen(purpose) ===
~ purpose_global = purpose //assigns chosen purpose into globals.ink
So you're a {purpose}? Hm. I'll be keeping an eye on you.
-> END

=== already_chosen ===
You already said you are a {purpose_global}. Now scram.

-> END


