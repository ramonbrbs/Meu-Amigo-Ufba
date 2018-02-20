<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Lingua;
use App\Usuario_lingua;

class LinguaController extends Controller
{
    public function index()
    {
        return Lingua::all();
    }

    public function store(Request $request)
    {
        $lingua = Usuario_lingua::create($request->all());
        return response()->json($lingua, 200);
    }
}
