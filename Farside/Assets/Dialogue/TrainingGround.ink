INCLUDE globals.ink

{ outpost_guard:
- 0: -> main
- 1: -> talked
}

=== main ===
~ outpost_guard = 1

Welcome to the village outpost, human.
I can't imagine you received a warm welcome from the rest of the guards, did you? 
Well, no matter. I'll be more than happy to answer your questions.
-> questions

== questions ===

 + [Where is this?]
This is the village outpost, if you can't already tell.
We have a watchtower and a training area here. There's also an underground farm below.
The farmers come here to hone their combat skills occasionally, though they're all working at the farm now.
Don't look surprised - that's because there are plenty of enemies ready to steal our crops, and fending them off takes serious skills.
Being a farmer sure is hard these days. That's why I chose to be an outpost guard instead!
Anything else?
-> questions

 + [Can I help?]
 Hm...let me think.
 I've got this place covered. Why don't you go check out the underground farm?
 I'm sure the farmers will be happy if you lend them a paw. Those <i>dratted</i> bat colonies won't leave our crops alone.
 Any other questions?
 -> questions
 
 + [That's all]
 Sure thing. Just a question - did you <i>really</i> tell the head guard you are a {purpose_global}?

-> END

=== talked ===
Our outpost isn't fancy, but it is something.
Do you have any other questions about this place, human {purpose_global}?

-> questions


-> END