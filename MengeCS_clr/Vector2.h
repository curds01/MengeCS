#pragma once

namespace MengeCS {
	public value class Vector2 {
	public:
		Vector2( float x, float y );
		//Vector2( const Vector2% v );
		static Vector2 Zero();
		float X();
		float Y();
	private:
		float _x;
		float _y;
	};
}