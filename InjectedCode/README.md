The code here is "injected" in the sense that it is a "shared project", and the
source files in the directory are compiled as part of each mod project, rather
than separately.

This is, sadly, necessary, because the Klei MOD loader won't cope with two
classes derived from `UserMod2`, even if one of them is abstract or something
like that.

So, my choices are to hand-write the mod loading logic in each project, or use
this model to get a single class injected into each of them that can be the more
generic base I want.
