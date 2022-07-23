INCLUDE globals.ink

{ quest_global == 0: -> main | -> quest }

=== main ===
~ quest_global = 1
Welcome, stranger, to our peaceful village.
I am the town head - that is to say, everything that goes on here is known to me.
Of course, the moment you stepped into our domain, I have known your true purpose.
However, what you want is not so easy to attain. But of course you know that.
If you would like to enter our village and learn its secrets, you must first complete a rite of passage.
Every traveler must prove their worth by aiding our folk in their troubles.
After all, we are but a small community, and we need each other's help to survive.
Come speak to me again after you have gained our people's vote of confidence.
Only then will you be allowed to set foot in our beloved village.

-> END

=== quest ===
Come speak to me again when you have our people's vote of confidence. 

* [What's my progress?]

{ helped_global:
- 0: No one has spoken about you.
Are you truly determined to enter the village?
- 1: One of the townsfolk regards you highly! Quite an improvement. 
But of course, you have a long way to go.
- 2: Two villagers have come to me with their recommendation. 
But alas, it takes more than two to tango. 
If I say so, then it is so. Get moving, human!
- else: Hm, you've made it this far! This is rather unexpected. Not that I doubted your abilities, of course. 
I'll see to it that you get a village permit right away. Once again, a warm welcome to our village of Farside... #end:yes
}

-> END

* [Alright]
Get moving, human. Time's ticking!

-> END













