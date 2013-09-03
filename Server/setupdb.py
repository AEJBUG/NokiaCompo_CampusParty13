import os
from database import *
from hashlib import sha512

__author__ = 'david'

# Delete file
os.remove('nokia.db')

# Create Tables
Users.create_table(fail_silently=True)
Stores.create_table(fail_silently=True)
Sessions.create_table(fail_silently=True)
Tags.create_table(fail_silently=True)
Categories.create_table(fail_silently=True)
StoreItems.create_table(fail_silently=True)
OrderItems.create_table(fail_silently=True)
ArchiveSessions.create_table(fail_silently=True)
ArchivedOrderItems.create_table(fail_silently=True)

# New user
user = Users()
user.username = 'admin'
user.password = sha512('admin').hexdigest()
user.fname = 'david'
user.lname = 'smith'
user.email = 'dasmith91@gmail.com'
user.isStore = True
user.save()

# New Store
store = Stores()
store.owner = user
store.name = "The Admiral - London"
store.save()

# Give the store some tags to use
for x in xrange(24):
    t = Tags()
    t.store = store
    t.save()

# Item Catagories
for x in ['breakfasts', 'lunches', 'dinners', 'soft drinks']:
    category = Categories()
    category.name = x
    category.save()

# StoreItems
items = [
    ['WARM CRAB & LEMON POT', "Delicious crab meat mixed with cream, onion & yoghurt topped with a lemon and herb crust, baked in the oven and served with brown bread"],
    ['SPICY KING PRAWNS', "Whole-tail Tiger prawns cooked in a mildly spicy batter and served with a sweet chilli dip"],
    ['STUFFED FIELD MUSHROOM', "Mushroom oven baked with a garlic and herb crust topped with goats cheese and dressed with secretts leaves served with a tangy garlic mayonnaise"],
    ['HOME MADE CHICKEN LIVER & SHERRY PATE', "A rustic pate served with onion marmalade and brown toast"],
    ['NUTTY FRIED BRIE WEDGES', "Somerset Brie wedges lightly fried in a nutty coating served with a cranberry relish"],
]

for x in items:
    i = StoreItems()
    i.store = store
    i.name = x[0].title()
    i.desc = x[1] + '.'
    i.category = Categories.select().first()
    i.price = 4.99
    i.save()

print ('all done!')