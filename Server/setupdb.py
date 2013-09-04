# coding=utf-8
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
for x in ['breakfasts', 'lunches', 'dinners', 'drinks']:
    category = Categories()
    category.name = x
    category.save()

# StoreItems
items = [
    # Breakfasts
    ['WARM CRAB & LEMON POT',
     "Delicious crab meat mixed with cream, onion & yoghurt topped with a lemon and herb crust, baked in the oven and served with brown bread",
     1, 4.99],
    ['SPICY KING PRAWNS', "Whole-tail Tiger prawns cooked in a mildly spicy batter and served with a sweet chilli dip",
     1, 4.99],
    ['STUFFED FIELD MUSHROOM',
     "Mushroom oven baked with a garlic and herb crust topped with goats cheese and dressed with secretts leaves served with a tangy garlic mayonnaise",
     1, 4.99],
    ['HOME MADE CHICKEN LIVER & SHERRY PATE', "A rustic pate served with onion marmalade and brown toast", 1, 4.99],
    ['NUTTY FRIED BRIE WEDGES', "Somerset Brie wedges lightly fried in a nutty coating served with a cranberry relish",
     1, 4.99],
    # Lunches

    # Dinners
    ['10OZ RUMPSTEAK',
     'Black Barn Farm 21 day matured large ten ounce succulent steak, griddled, served with thick cut chips, tomato, buttered field mushrooms and seasoned Spanish onion rings.',
     2, 14.99],
    ['RUMP OF LAMB',
     'Mint & Manuka honey glazed rump of Welsh lamb served in a truckle of spinach with a mint jus, freshly cooked vegetables and a choice of potatoes',
     2, 13.95],
    ['PORK MEDALLIONS',
     'Pan fried and served in a creamy peppercorn sauce, freshly cooked veg. and a choice of potatoes', 2, 10.95],
    ['LIVER AND BACON',
     'Tender slices of lamb’s liver, cooked together with Wiltshire cured bacon in a roasted onion gravy', 2, 9.70],
    ['BUTTERFLIED CHICKEN',
     'Tender chicken breast topped with a mushroom, bacon and tarragon cream sauce, fresh vegetables and a choice of potatoes',
     2, 10.95],
]

for x in items:
    i = StoreItems()
    i.store = store
    i.name = x[0].title()
    i.desc = x[1] + '.'
    i.category = Categories.select().where(Categories.id == x[2]).first()
    i.price = x[3]
    i.save()

print ('all done!')
