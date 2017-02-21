#include "stdafx.h"

#include "Vector2.h"

namespace MengeCS {

	Vector2::Vector2( float x, float y ) : _x( x ), _y( y ) {}
	//Vector2::Vector2( const Vector2% v ) : _x( v._x ), _y( v._y ) {}
	Vector2 Vector2::Zero() { return Vector2( 0.f, 0.f ); }
	float Vector2::X() { return _x; }
	float Vector2::Y() { return _y; }
}