INCLUDE globals.ink

{ outpost_guard:
- 0: -> main
- 1: -> talked
}

=== main ===
~ outpost_guard = 1

Welcome to the village outpost, human.
I can't imagine you received a warm welcome from the rest of the guards, did you? They're quite uptight.
Well, no matter. I'll be more than happy to answer your questions.
-> questions

== questions ===

 + [Where is this?]
This is a small area for training, if you can't already tell.
We don't have much resources, so we have to share some equipment with the farmers underground.
The farmers also come here to train occasionally.
You look surprised - but that's because there are plenty of enemies near the farm. 
That's why many of the farmers are also trained in combat.
Anything else?
-> questions

 + [Can I help?]
 Hm...
 I've got this place covered. Why don't you go check out the underground farm?
 I'm sure the farmers will be happy if you lend them a paw.
 Any other questions?
 -> questions
 
 + [That's all]
 Sure thing. Come back again anytime!

-> END

=== talked ===
Our outpost isn't fancy, but it is something.
Do you have any other questions about this place?

-> questions


-> END