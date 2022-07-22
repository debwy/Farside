INCLUDE globals.ink

{ purpose_global == "": -> main | -> already_chosen }

=== main ===
<i>Outpost is clear. No enemies sighted.</i>

State your purpose, human.
 + [I'm a visitor]
    -> chosen("visitor")
 + [I'm here to help]
    -> chosen("helper")
 + [I'm a spy]
    -> chosen("spy")
    

=== chosen(purpose) ===
~ purpose_global = purpose //assigns chosen purpose into globals.ink
So you're a <i>{purpose}</i>? Hm. I'll be keeping an eye on you.
-> END

=== already_chosen ===
You already said you are a <i>{purpose_global}</i>. Now scram.

{ brownmouse == 3: Now what's that suspicious look on your face? Stop smiling. It's creepy. }


-> END


