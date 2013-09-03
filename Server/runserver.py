from flask import Flask, render_template, session, flash, redirect, url_for

__author__ = 'david'

# Flask App
app = Flask(__name__)
app.secret_key = "a^h*+0ep6fy4^z+tmp(1n(sxdx)@6anuoz=-putttrfh@^kmor" # Ssshh! TOP SECRET! :p

# Database
# TODO: Set up MongoClient
# db = MongoClient()

# Routes
@app.route('/')
def v_index():
    return render_template('v_index.html')


# SERVERY STUFF
@app.route('/auth', methods=['GET, POST'])
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


@app.route('/api/opensession/<int:id>')
def s_opensession(id):
    if 'id' in session:
        flash('OPENING SESSION ON TABLE ID: %s' % id)
    else:
        flash('Sorry, you must be logged in to perform this action.')
    return redirect(url_for('v_index'))
    # TODO: write authentication


if __name__ == '__main__':
    app.run(threaded=True, debug=True)