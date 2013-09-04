import datetime
from peewee import Model, SqliteDatabase, CharField, BooleanField, FloatField, ForeignKeyField, DateTimeField, IntegerField, PostgresqlDatabase, MySQLDatabase

__author__ = 'david'


class Base(Model):
    class Meta:
        database = SqliteDatabase('nokia.db')
        # database = MySQLDatabase('nokia.db')
        # database = PostgresqlDatabase('nokia.db')


class Users(Base):
    username = CharField(unique=True, index=True)
    password = CharField()
    email = CharField(unique=True, index=True)
    fname = CharField()
    lname = CharField()
    isAdmin = BooleanField(default=False)
    isStore = BooleanField(default=False)

    def getNiceName(self):
        return "%s %s" % (self.fname, self.lname)

    def __unicode__(self):
        return self.username.upper()


class Stores(Base):
    name = CharField(unique=True)
    owner = ForeignKeyField(Users)
    lng = FloatField(null=True, default=None)
    lat = FloatField(null=True, default=None)

    def __unicode__(self):
        return self.name.upper()


class Tags(Base):
    store = ForeignKeyField(Stores, 'tags')

    def __unicode__(self):
        return '%d FOR STORE %s' % (self.id, self.store)


class Sessions(Base):
    tag = ForeignKeyField(Tags, related_name='session')
    timeOpened = DateTimeField()
    timeClosed = DateTimeField(null=True, default=None)

    def __unicode__(self):
        return self.id


class Categories(Base):
    name = CharField()

    def __unicode__(self):
        return self.name.upper()


class StoreItems(Base):
    store = ForeignKeyField(Stores, related_name='storeitems')
    category = ForeignKeyField(Categories)
    name = CharField()
    desc = CharField()
    price = FloatField()

    def __unicode__(self):
        return '%s FOR STORE %s' % (self.name.upper(), self.store.name.upper())


class OrderItems(Base):
    session = ForeignKeyField(Sessions, related_name='orderitems')
    item = ForeignKeyField(StoreItems)
    quantity = IntegerField()

    def __unicode__(self):
        return '%d of %s' % (self.quantity, self.item.first().name.upper())


class ArchiveSessions(Base):
    tag = ForeignKeyField(Tags, related_name='archivesessions')
    timeOpened = DateTimeField()
    timeClosed = DateTimeField()


class ArchivedOrderItems(Base):
    session = ForeignKeyField(ArchiveSessions, related_name='archivedordergtitems')
    item = ForeignKeyField(StoreItems)
    itemPrice = FloatField()
    quantity = IntegerField()