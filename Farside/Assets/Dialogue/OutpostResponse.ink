INCLUDE globals.ink

{purpose_global == "": -> main | -> response}


=== main ===
Squeak... I always stay here and hide. No, I'm not eavesdropping or anything!

-> END

=== response ===
<size=35><i>Squeak...</i></size>

{brownmouse:
- 1: -> already_accepted
- 2: -> completed
- 3: -> final
}

<size=35>I heard you say you were a {purpose_global}. I wasn't listening on purpose, really!</size>
<size=35>It's just...is it true?</size>

* [Yes]
* [No]

- <size=35>Oh, ok... </size>
~ brownmouse = 1
<size=35>Actually, I have a favour to ask? Even though you're a {purpose_global} and all?</size>
<size=35>Just figured asking someone who looks strong would be easier, you know?</size>
<size=35>If you're willing, could you help me defeat some enemies?</size>
<size=35>I don't know how many would be good, though.  Maybe <color=yellow>15 enemies of any type</color> would be great for a start!</size>
<size=35>I might even get you that recommendation if you complete this favour.</size>
<size=35>If you're worried about running out of enemies, don't worry!</size>
<size=35>They all return once you come back to the map. What a pain, right?</size>

-> END

=== already_accepted ===
<size=35><i>She looks amazing as always...so strong...but I wish she would relax a little...</i>
<size=35>Oh, you're still here? I was thinking that getting rid of <color=yellow>15 enemies of any type</color> would be great!</size>

{ totalDeaths_global >= 15: -> completed }

-> END

=== completed ===
//to change global variable brownmouse = 2 externally from game to come here

~ helped_global += 1
~ brownmouse = 3
<size=35>Oh my, blur old me! You've already done it! Thank you so much!
 * [The recommendation?]
    <size=35>I've got it! Done and over. Thanks for your help, human.</size>
 
<size=35>I hope this made her day. But don't tell her about this! </size>You absolutely can't!

-> END

=== final ===
<size=35>Thanks for your help, human! But don't you dare say a word to her!</size>

-> END





