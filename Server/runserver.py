from collections import defaultdict
import datetime
import json
import dicttoxml
from flask import Flask, render_template, session, flash, redirect, url_for, request
from database import Sessions, Tags, ArchiveSessions, StoreItems

__author__ = 'david'

# Flask App
app = Flask(__name__)
app.secret_key = "a^h*+0ep6fy4^z+tmp(1n(sxdx)@6anuoz=-putttrfh@^kmor" # Ssshh! TOP SECRET! :p

# Helpers
def hexToDec(i):
    return int('0x' + str(i), 0)

# Routes
@app.route('/')
def v_index():
    return render_template('v_index.html')


@app.route('/sessions')
def v_viewsessions():
    return ''
    # TODO: implement this

# NEW SERVER STUFF
@app.route('/api/order', methods=["POST"])
def s_order():
    data = request.data # but data will be empty unless the request has the proper content-type header...
    if not data:
        data = request.form.keys()[0]
    session["order"] = json.loads(data)
    return " YOU ORDERED FOOD YOU FAT SHITE %s" % data


@app.route('/api/<id>')
def s_tag(id=None):
    tag = Tags.select().where(Tags.id == id).first()
    session['id'] = tag.id
    storeItems = StoreItems.select().where(StoreItems.store == tag.store)

    dump = defaultdict(list)

    for x in storeItems:
        dump[x.category.name].append({"id": x.id, "name": x.name, "price": x.price, "desc": x.desc})
    return json.dumps(dump)


# SERVERY STUFF
@app.route('/auth', methods=['GET', 'POST'])
def s_auth():
    """ This method is used to auth with the server, please perform a POST to log in and GET to log out
        Logging in requires credentials, logging out requires hitting the page, any creds will be disreguarded
    """
    # USER NOT LOGGED IN, ATTEMPT AUTH
    if 'id' not in session:
        session['id'] = 1
        flash('You have been logged in.')
    else:
        # DISREGARD CURRENCY, ACQUIRE WENCHES
        session.clear()
        flash('You have been logged out.')
    return redirect(url_for('v_index'))

# API STUFF
@app.route('/api/getsessions')
def s_getsessions():
    rv = '<pre>'
    for x in Sessions.select():
        rv += 'SESSION ' + str(x.id) + '\n'
    rv += '</pre>'
    return rv


@app.route('/api/startsession/<i>')
def s_startsession(i):
    # Check to see if session exists ? Return session data : create a new one
    # tag = Tags.select().where(Tags.id == str(hexToDec(i))).first()
    tag = Tags.select().where(Tags.id == i).first()
    if tag is not None:
        if tag.session.first() is None:
            # No session for this ID exists, create one
            s = Sessions()
            s.timeOpened = datetime.datetime.now()
            s.tag = tag
            s.save()

        # Format menu for consumption
        data = {'menu': []}
        for x in StoreItems.select().where(StoreItems.store == tag.store):
            data['menu'].append({'name': x.name, 'desc': x.desc, 'id': x.id, 'price': x.price})
        return str(dicttoxml.dicttoxml(data, ids=False))
    else:
        flash('Sorry, that tag does not exist')
        return redirect(url_for('v_index'))


@app.route('/api/endsession/<i>')
def s_endsession(i):
    tag = Tags.select().where(Tags.id == i).first()
    if tag.session.first() is not None:
        s = tag.session.first()
        s.timeClosed = datetime.datetime.now()
        s.save()

        # Now migrate it
        arse = ArchiveSessions()
        arse.tag = tag
        arse.timeOpened = s.timeOpened
        arse.timeClosed = s.timeClosed
        arse.save()

        # Now delete the old one
        # Sessions.
        return 'Tag %d\'s session was closed!' % tag.id
    else:
        return 'Tag %d has no associated session!' % tag.id


@app.route('/api/orderitems/<tag>/<items>', methods=['GET', 'POST'])
def s_orderitems():
    items = [x.split(':') for x in items.split(',')]
    pass
    # TODO: implement a method of sending an order to the server


@app.route('/api/callstaff')
def s_callstaff():
    pass
    # TODO: implement a method of requesting a member of staff


if __name__ == '__main__':
    app.run(debug=True, host="0.0.0.0")