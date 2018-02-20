<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Interesse;
use App\Usuario_interesse;

class InteresseController extends Controller
{
    public function index()
    {
        return Interesse::all();
    }

    public function store(Request $request)
    {
        $interesse = Usuario_interesse::create($request->all());
        return response()->json($interesse, 200);
    }
}
