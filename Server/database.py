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
    lng = FloatField(null=True, default=None)
    lat = FloatField(null=True, default=None)


class Tags(Base):
    store = ForeignKeyField(Stores, 'tags')


class Sessions(Base):
    tag = ForeignKeyField(Tags, related_name='session')
    timeOpened = DateTimeField(default=datetime.datetime.now())
    timeClosed = DateTimeField(null=True, default=None)


class Catagories(Base):
    name = CharField()


class StoreItems(Base):
    store = ForeignKeyField(Stores, related_name='storeitems')
    catagories = ForeignKeyField(Catagories)
    name = CharField()
    price = FloatField()


class OrderItems(Base):
    session = ForeignKeyField(Sessions, related_name='orderitems')
    item = ForeignKeyField(StoreItems)
    quantity = IntegerField()


class ArchiveSessions(Base):
    tag = ForeignKeyField(Tags, related_name='archivesessions')
    timeOpened = DateTimeField()
    timeClosed = DateTimeField()


class ArchivedOrderItems(Base):
    session = ForeignKeyField(ArchiveSessions, related_name='archivedordergtitems')
    item = ForeignKeyField(StoreItems)
    itemPrice = FloatField()
    quantity = IntegerField()


if __name__ == '__main__':
    Users.create_table(fail_silently=True)
    Stores.create_table(fail_silently=True)
    Sessions.create_table(fail_silently=True)
    Tags.create_table(fail_silently=True)
    Catagories.create_table(fail_silently=True)
    StoreItems.create_table(fail_silently=True)
    OrderItems.create_table(fail_silently=True)
    ArchiveSessions.create_table(fail_silently=True)
    ArchivedOrderItems.create_table(fail_silently=True)