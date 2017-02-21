#include "stdafx.h"

#include "Agent.h"
#include <cassert>
#include <sstream>

namespace MengeCS {

	// ---------------------------------------------------

	Vector2 Agent::GetPosition() {
		assert( _agent != 0x0 );
		const Menge::Math::Vector2 & p = _agent->_pos;
		return Vector2( p._x, p._y );
	}
	
	// ---------------------------------------------------

	Vector2 Agent::GetVelocity() {
		assert( _agent != 0x0 );
		const Menge::Math::Vector2 & p = _agent->_vel;
		return Vector2( p._x, p._y );
	}
	
	// ---------------------------------------------------

	Vector2 Agent::GetOrientation() {
		assert( _agent != 0x0 );
		const Menge::Math::Vector2 & p = _agent->_orient;
		return Vector2( p._x, p._y );
	}
	
	// ---------------------------------------------------

	Vector2 Agent::GetPrefVelocity() {
		assert( _agent != 0x0 );
		const Menge::Math::Vector2 & p = _agent->_velPref.getPreferredVel();
		return Vector2( p._x, p._y );
	}
	
	// ---------------------------------------------------

	size_t Agent::GetClass() {
		assert( _agent != 0x0 );
		return _agent->_class;
	}

	// ---------------------------------------------------

	String^ Agent::ToString() {
		assert( _agent != 0x0 );
		std::stringstream ss;
		ss << "Agent " << _agent->_id;
		return gcnew String( ss.str().c_str() );
	}

	// ---------------------------------------------------

	Agent::Agent( const Menge::Agents::BaseAgent * agent ) : _agent( agent ) {
	}
}